using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Identifiers;
using Apps.XTRF.Classic.Models.Requests.ClassicQuote;
using Apps.XTRF.Classic.Models.Responses.ClassicQuote;
using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XTRF.Classic.Actions;

[ActionList]
public class ClassicQuoteActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : XtrfInvocable(invocationContext)
{
    #region Get

    [Action("Classic: Get quote", Description = "Get information about classic quote")]
    public async Task<QuoteResponse> GetQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}", Method.Get, Creds);
        var quote = await Client.ExecuteWithErrorHandling<ClassicQuote>(request);
        var timeZoneInfo = await GetTimeZoneInfo();
        return new(quote, timeZoneInfo);
    }

    #endregion

    #region Post

    [Action("Classic: Create language combination for quote", Description = "Create a new language combination for a " +
                                                                            "classic quote without creating a task")]
    public async Task<QuoteIdentifier> CreateLanguageCombinationForProject(
        [ActionParameter] QuoteIdentifier quoteIdentifier, 
        [ActionParameter] LanguageCombinationIdentifier languageCombination)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}", Method.Post, Creds)
            .WithJsonBody(new
            {
                sourceLanguageId = ConvertToInt64(languageCombination.SourceLanguageId, "Source language"),
                targetLanguageId = ConvertToInt64(languageCombination.TargetLanguageId, "Target language")
            });
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }
    
    [Action("Classic: Create quote", Description = "Create a new classic quote")]
    public async Task<CustomerQuoteResponse> CreateQuote([ActionParameter] QuoteCreateRequest request)
    {
        var customerActions = new CustomerActions(invocationContext);
        var contactPerson = await customerActions.GetContactPerson(request);
        var tokenResponse = await GetPersonAccessToken(contactPerson?.Contact?.Emails?.Primary ?? throw new Exception("Contact person has no primary email."));
        var customerPortalClient = GetCustomerPortalClient(tokenResponse.Token);
        var officeId = (await GetDefaultOffice(contactPerson.Contact.Emails.Primary)).Id;
        
        var obj = new
        {
            name = request.QuoteName,
            customerProjectNumber = request.CustomerProjectNumber,
            serviceId = int.Parse(request.ServiceId),
            sourceLanguageId = int.Parse(request.SourceLanguageId),
            targetLanguageIds = request.TargetLanguageIds.Select(int.Parse).ToList(),
            specializationId = int.Parse(request.SpecializationId),
            deliveryDate = new
            {
                time = request.DeliveryDate.HasValue 
                    ? new DateTimeOffset(request.DeliveryDate.Value).ToUnixTimeMilliseconds() 
                    : DateTime.Now.AddDays(7).ToUnixTimeMilliseconds()
            },
            notes = request.Note ?? string.Empty,
            priceProfileId = int.Parse(request.PriceProfileId),
            personId = int.Parse(request.PersonId),
            sendBackToId = request.SendBackToId == null ? int.Parse(request.PersonId) : int.Parse(request.SendBackToId),
            additionalPersonIds = request.AdditionalPersonIds == null 
                ? new List<int>() 
                : request.AdditionalPersonIds.Select(int.Parse).ToList(),
            files = request.Files == null 
                ? new List<FileUpload>() 
                : await customerPortalClient.UploadFilesAsync(request.Files, fileManagementClient),
            referenceFiles = request.ReferenceFiles == null 
                ? new List<FileUpload>() 
                : await customerPortalClient.UploadFilesAsync(request.ReferenceFiles, fileManagementClient),
            customFields = new List<string>(),
            officeId = request.OfficeId != null 
                ? int.Parse(request.OfficeId) 
                : officeId,
            budgetCode = request.BudgetCode ?? string.Empty,
            catToolType = request.CatToolType ?? "TRADOS"
        };
        
        var quoteDto = await customerPortalClient.ExecuteRequestAsync<Quote>("/v2/quotes", Method.Post, obj);
        return new(quoteDto);
    }

    [Action("Classic: Start quote", Description = "Start a classic quote")]
    public async Task<QuoteIdentifier> StartQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}/start", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }
    
    [Action("Classic: Send quote for customer confirmation", Description = "Send a quote for customer confirmation, " +
                                                                           "changing the quote status to \"Sent\"")]
    public async Task<QuoteIdentifier> SendQuoteForConfirmation([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}/confirmation/send", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }

    #endregion

    #region Put

    [Action("Classic: Update quote instructions", Description = "Update instructions of a given quote, specifying " +
                                                                "only the fields that require updating")]
    public async Task<QuoteIdentifier> UpdateQuoteInstructions([ActionParameter] QuoteIdentifier quoteIdentifier, 
        [ActionParameter] UpdateQuoteInstructionsRequest input)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}/instructions", Method.Put, Creds)
            .WithJsonBody(new
            {
                fromCustomer = input.InstructionFromCustomer,
                forProvider = input.InstructionForProvider,
                Internal = input.InternalInstruction,
                paymentNoteForCustomer = input.PaymentNoteForCustomer,
                notes = input.Notes
            }, JsonConfig.Settings);
        
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }

    #endregion

    #region Delete

    [Action("Classic: Delete quote", Description = "Delete a quote")]
    public async Task DeleteQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    #endregion
    
    #region Utils
    
    public async Task<FullOfficeDto> GetDefaultOffice(string emailOrLogin)
    {
        var accessToken = await GetPersonAccessToken(emailOrLogin);
        var customerPortalClient = GetCustomerPortalClient(accessToken.Token);
        var officeDto = await customerPortalClient.ExecuteRequestAsync<FullOfficeDto>($"/offices/default", Method.Get, null);
        return officeDto;
    }
    
    public async Task<FullOfficeDto> GetOfficeById(string officeId, XtrfCustomerPortalClient customerPortalClient)
    {
        var officeDto = await customerPortalClient.ExecuteRequestAsync<FullOfficeDto>($"/offices/{officeId}", Method.Get, null);
        return officeDto;
    }
    
    #endregion
}