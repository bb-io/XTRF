using Apps.XTRF.Requests;
using Apps.XTRF.Responses;
using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Actions
{
    [ActionList]
    public class QuoteActions
    {
        [Action("Get quote details", Description = "Get all information of a specific quote")]
        public Quote GetQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId, Method.Get, authenticationCredentialsProviders);
            return client.Get<Quote>(request);
        }

        [Action("Create new quote", Description = "Create a new quote")]
        public Quote CreateQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] SimpleQuote quote)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(quote);
            return client.Post<Quote>(request);
        }

        [Action("Get jobs in a quote", Description = "Get all jobs of a specific quote")]
        public GetJobsResponse GetJobsByQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/jobs", Method.Get, authenticationCredentialsProviders);
            return new GetJobsResponse()
            {
                Jobs = client.Get<List<Job>>(request)
            };
        }

        [Action("Get files in a quote", Description = "Get all files of a specific quote")]
        public GetFilesResponse GetFilesByQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/files", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.Get<List<FileXTRF>>(request)
            };
        }

        [Action("Upload a file to a quote", Description = "Upload a file to a specific quote")]
        public void UploadFileToQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] UploadFileToQuoteRequest input)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var uploadRequest = new XtrfRequest("/v2/quotes/" + input.QuoteId + "/files/upload", Method.Post, authenticationCredentialsProviders);
            uploadRequest.AddFile("file", input.File, input.FileName);
            var outputFileId = client.Post<UploadFileResponse>(uploadRequest).FileId;

            var addRequest = new XtrfRequest("/v2/quotes/" + input.QuoteId + "/files/add", Method.Put, authenticationCredentialsProviders);
            addRequest.AddJsonBody(new
            {
                files = new[]
                {
                    new
                    {
                        category = input.Category,
                        fileId = outputFileId
                    }
                }
            });

            client.Execute(addRequest);

        }

        [Action("Get quote file details", Description = "Get details of a specific file in a quote")]
        public FileXTRF GetQuoteFileDetails(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string fileId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/files/" + fileId, Method.Get, authenticationCredentialsProviders);
            return client.Get<FileXTRF>(request);
        }

        [Action("Get finance information for a quote", Description = "Get finance information for a specific quote")]
        public FinanceInformation GetFinanceInfoForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/finance", Method.Get, authenticationCredentialsProviders);
            return client.Get<FinanceInformation>(request);
        }

        [Action("Delete a payable for a quote", Description = "Delete a payable for a specific quote")]
        public void DeletePayableForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] int payableId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/finance/payables/" + payableId, Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }

        [Action("Delete a receivable for a quote", Description = "Delete a receivable for a specific quote")]
        public void DeleteReceivableForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] int receivableId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/finance/receivables/" + receivableId, Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }

        [Action("Update business days for a quote", Description = "Update business days for a specific quote")]
        public void UpdateBusinessDaysForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] int businessDays)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/businessDays", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = businessDays
            });
            client.Execute(request);
        }

        [Action("Update client notes for a quote", Description = "Update client notes for a specific quote")]
        public void UpdateClientNotesForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] string clientNotes)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/clientNotes", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = clientNotes
            });
            client.Execute(request);
        }

        [Action("Update client reference number for a quote", Description = "Update client reference number for a specific quote")]
        public void UpdateClientReferenceNumberForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] string referenceNumber)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/clientReferenceNumber", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = referenceNumber
            });
            client.Execute(request);
        }

        [Action("Update expected delivery date for a quote", Description = "Update expected delivery date for a specific quote")]
        public void UpdateDeliveryDateForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] string deliveryDate)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/expectedDeliveryDate", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = ConvertStringToUnixTime(deliveryDate)
            });
            client.Execute(request);
        }

        [Action("Update internal notes for a quote", Description = "Update internal notes for a specific quote")]
        public void UpdateInternalNotesForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] string internalNotes)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/internalNotes", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = internalNotes
            });
            client.Execute(request);
        }

        [Action("Update expiry date for a quote", Description = "Update expiry date for a specific quote")]
        public void UpdateExpiryDateForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] string expiryDate)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/quoteExpiry", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = ConvertStringToUnixTime(expiryDate)
            });
            client.Execute(request);
        }

        [Action("Update source language for a quote", Description = "Update source language for a specific quote")]
        public void UpdateSourceLanguageForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] int sourceLanguageId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/sourceLanguage", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                sourceLanguageId = sourceLanguageId
            });
            client.Execute(request);
        }

        [Action("Update vendor instructions for a quote", Description = "Update vendor instructions for a specific quote")]
        public void UpdateVendorInstructionsForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] string vendorInstructions)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/vendorInstructions", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = vendorInstructions
            });
            client.Execute(request);
        }

        [Action("Update volume for a quote", Description = "Update volume for a specific quote")]
        public void UpdateVolumeForQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string quoteId, [ActionParameter] int volume)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/quotes/" + quoteId + "/volume", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = volume
            });
            client.Execute(request);
        }

        public long ConvertStringToUnixTime(string inputDate)
        {
            DateTime date = DateTime.Parse(inputDate).ToUniversalTime();
            var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);

            DateTimeOffset offset = new DateTimeOffset(unspecifiedDateKind);
            long unixTime = offset.ToUnixTimeMilliseconds();

            return unixTime;
        }
    }
}
