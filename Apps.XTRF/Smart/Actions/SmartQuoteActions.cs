using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Models.Entities;
using Apps.XTRF.Smart.Models.Identifiers;
using Apps.XTRF.Smart.Models.Requests.File;
using Apps.XTRF.Smart.Models.Requests.SmartQuote;
using Apps.XTRF.Smart.Models.Responses.File;
using Apps.XTRF.Smart.Models.Responses.SmartJob;
using Apps.XTRF.Smart.Models.Responses.SmartQuote;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Parsers;
using RestSharp;
using Apps.XTRF.Classic.Models.Requests.ClassicQuote;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.XTRF.Smart.Actions;

[ActionList]
public class SmartQuoteActions : BaseFileActions
{
    public SmartQuoteActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext, fileManagementClient)
    {
    }

    #region Get

    [Action("Smart: Get quote details", Description = "Get information about a smart quote.  If you need to retrieve " +
                                                      "finance information, set the respective optional parameter to 'True'")]
    public async Task<QuoteResponse> GetQuote([ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] [Display("Include finance information")] bool? includeFinanceInformation)
    {
        var getQuoteRequest = new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}", Method.Get, Creds);
        var quote = await Client.ExecuteWithErrorHandling<SmartQuote>(getQuoteRequest);
        var timeZoneInfo = await GetTimeZoneInfo();
        var quoteResponse = new QuoteResponse(quote, timeZoneInfo);

        if (includeFinanceInformation != null && includeFinanceInformation.Value)
        {
            var request = new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/finance", Method.Get, Creds);
            var financeInformation = await Client.ExecuteWithErrorHandling<FinanceInformation>(request);
            quoteResponse.FinanceInformation = financeInformation;
        }

        return quoteResponse;
    }

    [Action("Smart: Add receivable to quote",
      Description = "Adds a receivable line")]
    public async Task<QuoteIdentifier> AddReceivableToQuote(
      [ActionParameter] QuoteIdentifier quoteIdentifier,
      [ActionParameter] AddQuoteReceivableRequest input)
    {
        if (string.IsNullOrWhiteSpace(input.CalculationUnitId))
            throw new PluginMisconfigurationException("Calculation Unit ID is required.");
        if (input.Units <= 0)
            throw new PluginMisconfigurationException("Units must be greater than zero.");

        var payload = new
        {
            languageCombination = input.SourceLanguageId != null && input.TargetLanguageId != null
                ? new
                {
                    sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                    targetLanguageId = ConvertToInt64(input.TargetLanguageId, "Target language")
                }
                : null,
            languageCombinationIdNumber = input.LanguageCombinationIdNumber,
            jobTypeId = string.IsNullOrWhiteSpace(input.JobTypeId)
                ? null
                : ConvertToInt64(input.JobTypeId, "Job type"),

            type = input.Type ?? "SIMPLE",
            calculationUnitId = ConvertToInt64(input.CalculationUnitId, "Calculation unit"),
            quantity = input.Units,
            rate = input.Rate,
            rateOrigin = input.Rate.HasValue ? "CUSTOM" : "PRICE_PROFILE",
            currencyId = string.IsNullOrWhiteSpace(input.CurrencyId) ? null : ConvertToInt64(input.CurrencyId, "Currency"),
            ignoreMinimumCharge = input.IgnoreMinimumCharge,
            minimumCharge = input.MinimumCharge,
            description = input.Description
        };

        var req = new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/finance/receivables",
            Method.Post, Creds).WithJsonBody(payload, JsonConfig.Settings);

        await Client.ExecuteWithErrorHandling(req);
        return quoteIdentifier;
    }

    [Action("Smart: List jobs in quote", Description = "List all jobs in a smart quote")]
    public async Task<ListJobsResponse> GetJobsByQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/jobs", Method.Get, Creds);
        var jobs = await Client.ExecuteWithErrorHandling<IEnumerable<SmartJob>>(request);
        var timeZoneInfo = await GetTimeZoneInfo();
        return new(jobs.Select(job => new JobResponse(job, timeZoneInfo)));
    }
    
    [Action("Smart: List files in quote", Description = "List all files in a smart quote")]
    public async Task<ListFilesResponse> GetFilesByQuote([ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] FilterLanguageOptionalIdentifier languageIdentifier,
        [ActionParameter] SmartFileCategoryOptionalIdentifier fileCategoryIdentifier)
    {
        var request = new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/files", Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<IEnumerable<SmartFileXTRF>>(request);
        var filteredFiles = files.Where(file =>
                languageIdentifier.LanguageId is null ||
                file.LanguageRelation.Languages.Contains(languageIdentifier.LanguageId))
            .Where(file =>
                fileCategoryIdentifier.Category is null || file.CategoryKey == fileCategoryIdentifier.Category);
    
        return new(filteredFiles);
    }
    
    [Action("Smart: Get quote file details", Description = "Get information about specific file in a smart quote")]
    public async Task<SmartFileXTRF> GetQuoteFile([ActionParameter] FileIdentifier fileIdentifier)
    {
        var request = new XtrfRequest($"/v2/quotes/files/{fileIdentifier.FileId}", Method.Get, Creds);
        var file = await Client.ExecuteWithErrorHandling<SmartFileXTRF>(request);
        return file;
    }

    #endregion

    #region Post

    [Action("Smart: Create new quote", Description = "Create a new smart quote")]
    public async Task<QuoteResponse> CreateQuote([ActionParameter] CreateQuoteRequest input)
    {
        var request = new XtrfRequest("/v2/quotes", Method.Post, Creds)
            .WithJsonBody(new
            {
                name = input.Name,
                clientId = input.ClientId,
                serviceId = input.ServiceId,
                opportunityOfferId = input.OpportunityOfferId
            });

        var quote = await Client.ExecuteWithErrorHandling<SmartQuote>(request);
        var timeZoneInfo = await GetTimeZoneInfo();
        return new(quote, timeZoneInfo);
    }
    
    [Action("Smart: Upload file to quote", Description = "Upload a file to a smart quote")]
    public async Task UploadFileToQuote([ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] UploadFileRequest input)
    {
        var uploadFileRequest =
            new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/files/upload", Method.Post, Creds);
        var fileBytes = await DownloadFile(input.File);
        uploadFileRequest.AddFile("file", fileBytes, input.File.Name!.Sanitize());
        var fileIdentifier = await Client.ExecuteWithErrorHandling<FileIdentifier>(uploadFileRequest);

        var addFileRequest = new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/files/add", Method.Put, Creds)
            .WithJsonBody(new
            {
                files = new[]
                {
                    new
                    {
                        category = input.Category,
                        fileId = fileIdentifier.FileId,
                        languageIds = input.LanguageId is not null
                            ? new[] { IntParser.Parse(input.LanguageId, nameof(input.LanguageId)) }
                            : null,
                        languageCombinationIds = input.SourceLanguageId is not null
                                                 && input.TargetLanguageId is not null
                            ? new[]
                            {
                                new
                                {
                                    sourceLanguageId =
                                        IntParser.Parse(input.SourceLanguageId, nameof(input.SourceLanguageId)),
                                    targetLanguageId =
                                        IntParser.Parse(input.TargetLanguageId, nameof(input.TargetLanguageId))
                                }
                            }
                            : null
                    }
                }
            }, JsonConfig.Settings);

        await Client.ExecuteWithErrorHandling(addFileRequest);
    }

    #endregion

    #region Put

    [Action("Smart: Add target languages to quote", Description = "Add more target languages to a smart quote")]
    public async Task<QuoteIdentifier> AddTargetLanguageToProject(
        [ActionParameter] QuoteIdentifier quoteIdentifier, 
        [ActionParameter] [Display("Target languages")] [DataSource(typeof(LanguageDataHandler))] 
        IEnumerable<string> targetLanguageIds)
    {
        var getQuoteRequest = new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}", Method.Get, Creds);
        var quote = await Client.ExecuteWithErrorHandling<SmartQuote>(getQuoteRequest);
        var targetLanguages = quote.Languages.TargetLanguageIds.Union(targetLanguageIds);
        
        var updateTargetLanguagesRequest =
            new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/targetLanguages", Method.Put, Creds)
                .WithJsonBody(new
                {
                    targetLanguageIds = ConvertToInt64Enumerable(targetLanguages, "Target languages")
                });
        
        await Client.ExecuteWithErrorHandling(updateTargetLanguagesRequest);
        return quoteIdentifier;
    }

    [Action("Smart: Update quote", Description = "Update a smart quote, specifying only the fields that require updating")]
    public async Task<QuoteIdentifier> UpdateQuote([ActionParameter] QuoteIdentifier quoteIdentifier, 
        [ActionParameter] UpdateQuoteRequest input)
    {
        if (input.Status != null)
        {
            var updateStatusRequest = 
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/status", Method.Put, Creds)
                    .WithJsonBody(new { status = input.Status });
            
            await Client.ExecuteWithErrorHandling(updateStatusRequest);
        }

        if (input.SourceLanguageId != null)
        {
            var updateSourceLanguageRequest = 
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/sourceLanguage", Method.Put, Creds)
                    .WithJsonBody(new { sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language") });

            await Client.ExecuteWithErrorHandling(updateSourceLanguageRequest);
        }

        if (input.TargetLanguageIds != null)
        {
            var updateTargetLanguagesRequest =
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/targetLanguages", Method.Put, Creds)
                    .WithJsonBody(new
                    {
                        targetLanguageIds = ConvertToInt64Enumerable(input.TargetLanguageIds, "Target languages")
                    });

            await Client.ExecuteWithErrorHandling(updateTargetLanguagesRequest);
        }

        if (input.SpecializationId != null)
        {
            var updateSpecializationRequest =
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/specialization", Method.Put, Creds)
                    .WithJsonBody(new { specializationId = ConvertToInt64(input.SpecializationId, "Specialization") });
            
            await Client.ExecuteWithErrorHandling(updateSpecializationRequest);
        }

        if (input.PrimaryId != null || input.AdditionalIds != null)
        {
            object requestBody;

            if (input.PrimaryId == null || input.AdditionalIds == null)
            {
                var getContactsRequest =
                    new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/clientContacts", Method.Get, Creds);
                var contacts = await Client.ExecuteWithErrorHandling<SmartProjectContacts>(getContactsRequest);
                requestBody = new
                {
                    primaryId = ConvertToInt64(input.PrimaryId ?? contacts.PrimaryId, "Primary contact person"),
                    additionalIds = ConvertToInt64Enumerable(input.AdditionalIds ?? contacts.AdditionalIds,
                        "Additional contact persons")
                };
            }
            else
                requestBody = new
                {
                    primaryId = ConvertToInt64(input.PrimaryId, "Primary contact person"),
                    additionalIds = ConvertToInt64Enumerable(input.AdditionalIds, "Additional contact persons")
                };

            var updateContactsRequest =
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/clientContacts", Method.Put, Creds)
                    .WithJsonBody(requestBody);
            
            await Client.ExecuteWithErrorHandling(updateContactsRequest);
        }

        if (input.BusinessDays != null)
        {
            var updateBusinessDaysRequest =
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/businessDays", Method.Put, Creds)
                    .WithJsonBody(new { value = input.BusinessDays });

            await Client.ExecuteWithErrorHandling(updateBusinessDaysRequest);
        }
        
        if (input.ClientNotes != null)
        {
            var updateClientNotesRequest =
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/clientNotes", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientNotes });

            await Client.ExecuteWithErrorHandling(updateClientNotesRequest);
        }

        if (input.InternalNotes != null)
        {
            var updateInternalNotesRequest = 
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/internalNotes", Method.Put, Creds)
                    .WithJsonBody(new { value = input.InternalNotes });

            await Client.ExecuteWithErrorHandling(updateInternalNotesRequest);
        }

        if (input.VendorInstructions != null)
        {
            var updateVendorInstructionsRequest = 
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/vendorInstructions", Method.Put, Creds)
                    .WithJsonBody(new { value = input.VendorInstructions });

            await Client.ExecuteWithErrorHandling(updateVendorInstructionsRequest);
        }

        if (input.ClientReferenceNumber != null)
        {
            var updateClientReferenceNumberRequest =
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/clientReferenceNumber", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientReferenceNumber });

            await Client.ExecuteWithErrorHandling(updateClientReferenceNumberRequest);
        }

        if (input.ExpectedDeliveryDate != null)
        {
            var timeZoneInfo = await GetTimeZoneInfo();
            var updateExpectedDeliveryDateRequest = 
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/expectedDeliveryDate", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ExpectedDeliveryDate?.ConvertToUnixTime(timeZoneInfo) });

            await Client.ExecuteWithErrorHandling(updateExpectedDeliveryDateRequest);
        }

        if (input.QuoteExpiry != null)
        {
            var timeZoneInfo = await GetTimeZoneInfo();
            var updateQuoteExpiryRequest = 
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/quoteExpiry", Method.Put, Creds)
                    .WithJsonBody(new { value = input.QuoteExpiry?.ConvertToUnixTime(timeZoneInfo) });

            await Client.ExecuteWithErrorHandling(updateQuoteExpiryRequest);
        }

        if (input.Volume != null)
        {
            var updateVolumeRequest =
                new XtrfRequest($"/v2/quotes/{quoteIdentifier.QuoteId}/volume", Method.Put, Creds)
                    .WithJsonBody(new { value = input.Volume });

            await Client.ExecuteWithErrorHandling(updateVolumeRequest);
        }

        return quoteIdentifier;
    }

    #endregion

    #region Delete

    [Action("Smart: Delete payable for quote", Description = "Delete a payable for a smart quote")]
    public async Task DeletePayableForProject([ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] SmartPayableIdentifier payableIdentifier)
    {
        var request =
            new XtrfRequest(
                $"/v2/quotes/{quoteIdentifier.QuoteId}/finance/payables/{payableIdentifier.PayableId}",
                Method.Delete, Creds);
        
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Smart: Delete receivable for quote", Description = "Delete a receivable for a smart quote")]
    public async Task DeleteReceivableForProject([ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] SmartReceivableIdentifier receivableIdentifier)
    {
        var request =
            new XtrfRequest(
                $"/v2/quotes/{quoteIdentifier.QuoteId}/finance/receivables/{receivableIdentifier.ReceivableId}",
                Method.Delete, Creds);
        
        await Client.ExecuteWithErrorHandling(request);
    }
    
    #endregion
}