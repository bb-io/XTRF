using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Requests.CustomField;
using Apps.XTRF.Shared.Models.Responses.CustomField;
using Apps.XTRF.Smart.Models.Entities;
using Apps.XTRF.Smart.Models.Requests.Quote;
using Apps.XTRF.Smart.Models.Responses.File;
using Apps.XTRF.Smart.Models.Responses.SmartJob;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Parsers;
using RestSharp;

namespace Apps.XTRF.Smart.Actions;

[ActionList]
public class QuoteActions : XtrfInvocable
{
    public QuoteActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get quote details", Description = "Get all information of a specific quote")]
    public Task<Quote> GetQuote([ActionParameter] [Display("Quote ID")] string quoteId)
    {
        var endpoint = "/v2/quotes/" + quoteId;
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<Quote>(request);
    }

    [Action("Create new quote", Description = "Create a new quote")]
    public Task<Quote> CreateQuote([ActionParameter] CreateQouteInput quote)
    {
        var request = new XtrfRequest("/v2/quotes", Method.Post, Creds);
        request.AddJsonBody(new CreateQouteRequest(quote));

        return Client.ExecuteWithErrorHandling<Quote>(request);
    }

    [Action("Get jobs in a quote", Description = "Get all jobs of a specific quote")]
    public async Task<ListJobsResponse> GetJobsByQuote([ActionParameter] [Display("Quote ID")] string quoteId)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/jobs";
        var request = new XtrfRequest(endpoint, Method.Get,
            Creds);
        var jobs = await Client.ExecuteWithErrorHandling<List<SmartJob>>(request);

        return new(jobs.Select(job => new JobResponse(job)));
    }

    [Action("Get files in a quote", Description = "Get all files of a specific quote")]
    public async Task<ListFilesResponse> GetFilesByQuote([ActionParameter] [Display("Quote ID")] string quoteId)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/files";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<List<SmartFileXTRF>>(request);
        return new(files);
    }

    [Action("Upload a file to a quote", Description = "Upload a file to a specific quote")]
    public async Task UploadFileToQuote([ActionParameter] UploadFileToQuoteRequest input)
    {
        var uploadEndpoint = "/v2/quotes/" + input.QuoteId + "/files/upload";
        var uploadRequest = new XtrfRequest(uploadEndpoint, Method.Post, Creds);

        uploadRequest.AddFile("file", input.File.Bytes, input.FileName?.Trim() ?? input.File.Name);
        var outputFileId = (await Client.ExecuteWithErrorHandling<UploadFileResponse>(uploadRequest)).FileId;

        var addEndpoint = "/v2/quotes/" + input.QuoteId + "/files/add";
        var addRequest = new XtrfRequest(addEndpoint, Method.Put, Creds);
        addRequest.AddJsonBody(new
        {
            files = new[]
            {
                new
                {
                    category = input.Category,
                    fileId = outputFileId,
                    languageIds = input.LanguageId is not null
                        ? new[] { IntParser.Parse(input.LanguageId, nameof(input.LanguageId))!.Value }
                        : null,
                    languageCombinationIds = input.SourceLanguageId is not null
                                             && input.TargetLanguageId is not null
                        ? new[]
                        {
                            new
                            {
                                sourceLanguageId =
                                    IntParser.Parse(input.SourceLanguageId, nameof(input.SourceLanguageId)),
                                targetLanguageId = IntParser.Parse(input.TargetLanguageId,
                                    nameof(input.TargetLanguageId))
                            }
                        }
                        : null
                }
            }
        });

        await Client.ExecuteWithErrorHandling(addRequest);
    }

    [Action("Get quote file details", Description = "Get details of a specific file in a quote")]
    public Task<SmartFileXTRF> GetQuoteFileDetails([ActionParameter] [Display("File ID")] string fileId)
    {
        var endpoint = "/v2/quotes/files/" + fileId;
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<SmartFileXTRF>(request);
    }

    [Action("Get finance information for a quote", Description = "Get finance information for a specific quote")]
    public Task<FinanceInformation> GetFinanceInfoForQuote([ActionParameter] [Display("Quote ID")] string quoteId)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/finance";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<FinanceInformation>(request);
    }

    [Action("Delete a payable for a quote", Description = "Delete a payable for a specific quote")]
    public Task DeletePayableForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Payable ID")]
        string payableId)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/finance/payables/" + payableId;
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Delete a receivable for a quote", Description = "Delete a receivable for a specific quote")]
    public Task DeleteReceivableForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Receivable ID")]
        string receivableId)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/finance/receivables/" + receivableId;
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update business days for a quote", Description = "Update business days for a specific quote")]
    public Task UpdateBusinessDaysForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Business days")]
        int businessDays)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/businessDays";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = businessDays
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update client notes for a quote", Description = "Update client notes for a specific quote")]
    public Task UpdateClientNotesForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Client notes")]
        string clientNotes)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/clientNotes";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = clientNotes
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update client reference number for a quote",
        Description = "Update client reference number for a specific quote")]
    public Task UpdateClientReferenceNumberForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Reference number")]
        string referenceNumber)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/clientReferenceNumber";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = referenceNumber
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update expected delivery date for a quote",
        Description = "Update expected delivery date for a specific quote")]
    public Task UpdateDeliveryDateForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Delivery date")] DateTime deliveryDate)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/expectedDeliveryDate";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = deliveryDate.ConvertToUnixTime()
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update internal notes for a quote", Description = "Update internal notes for a specific quote")]
    public Task UpdateInternalNotesForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Internal notes")] string internalNotes)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/internalNotes";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = internalNotes
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update expiry date for a quote", Description = "Update expiry date for a specific quote")]
    public Task UpdateExpiryDateForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Expiry date")] DateTime expiryDate)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/quoteExpiry";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = expiryDate.ConvertToUnixTime()
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update source language for a quote", Description = "Update source language for a specific quote")]
    public Task UpdateSourceLanguageForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [DataSource(typeof(LanguageDataHandler))] [Display("Source language")]
        string sourceLanguageId)
    {
        if (!int.TryParse(sourceLanguageId, out var intSourceLangId))
            throw new("Source language ID must be a number");

        var endpoint = "/v2/quotes/" + quoteId + "/sourceLanguage";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            sourceLanguageId = intSourceLangId
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update target languages for a quote", Description = "Update target languages for a specific quote")]
    public Task UpdateTargetLanguagesForQuote([ActionParameter] UpdateQuoteTargetLanguagesRequest input)
    {
        var endpoint = "/v2/quotes/" + input.QuoteId + "/targetLanguages";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            targetLanguageIds = input.TargetLanguageIds
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update vendor instructions for a quote", Description = "Update vendor instructions for a specific quote")]
    public Task UpdateVendorInstructionsForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Vendor instructions")] string vendorInstructions)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/vendorInstructions";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = vendorInstructions
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update volume for a quote", Description = "Update volume for a specific quote")]
    public Task UpdateVolumeForQuote([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] [Display("Volume")] int volume)
    {
        var endpoint = "/v2/quotes/" + quoteId + "/volume";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = volume
        });

        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("List quote custom fields", Description = "List custom fields of a specific quote")]
    public async Task<ListCustomFieldsResponse> ListQuoteCustomFields(
        [ActionParameter] [Display("Quote ID")] string quoteId)
    {
        var endpoint = $"/v2/quotes/{quoteId}/customFields";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        var response = await Client.ExecuteWithErrorHandling<CustomField[]>(request);
        return new(response);
    }

    [Action("Update quote custom field", Description = "Update custom field of a specific quote")]
    public Task UpdateQuoteCustomField([ActionParameter] [Display("Quote ID")] string quoteId,
        [ActionParameter] UpdateCustomFieldInput input)
    {
        var endpoint = $"/v2/quotes/{quoteId}/customFields/{input.Key}";
        var request = new XtrfRequest(endpoint, Method.Put, Creds)
            .WithJsonBody(new UpdateCustomFieldRequest(input.Value), JsonConfig.Settings);

        return Client.ExecuteWithErrorHandling(request);
    }
}