using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Requests.Invoice;
using Apps.XTRF.Shared.Models.Responses.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Shared.Actions;

[ActionList("Provider invoices")]
public class ProviderInvoiceActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : XtrfInvocable(invocationContext)
{
    [Action("Search provider invoices", Description = "Search for provider invoices based on the given criteria")]
    public async Task<ProviderInvoiceSearchResponse> SearchProviderInvoicesAsync([ActionParameter] ProviderInvoiceSearchRequest request)
    {
        var xtrfRequest = new XtrfRequest("/accounting/providers/invoices", Method.Get, Creds);
        if (request.UpdatedSince.HasValue)
        {
            var unixTimestamp = ((DateTimeOffset)request.UpdatedSince.Value).ToUnixTimeMilliseconds();
            xtrfRequest.AddQueryParameter("updatedSince", unixTimestamp.ToString());
        }
        
        var invoices = await Client.ExecuteWithErrorHandling<List<ProviderInvoiceResponse>>(xtrfRequest);
        return new()
        {
            Invoices = invoices
        };
    }
    
    [Action("Get provider invoice", Description = "Get provider invoice by ID")]
    public async Task<ProviderInvoiceResponse> GetProviderInvoiceAsync([ActionParameter] ProviderInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/providers/invoices/{request.ProviderInvoiceId}", Method.Get, Creds);
        var invoice = await Client.ExecuteWithErrorHandling<ProviderInvoiceResponse>(xtrfRequest);
        
        var paymentRequest = new XtrfRequest($"/accounting/providers/invoices/{request.ProviderInvoiceId}/payments", Method.Get, Creds);
        var payments = await Client.ExecuteWithErrorHandling<List<PaymentResponse>>(paymentRequest);
        invoice.Payments = payments;
        
        return invoice;
    }
    
    [Action("Create provider invoice", Description = "Create provider invoice")]
    public async Task<ProviderInvoiceResponse> CreateProviderInvoiceAsync([ActionParameter] ProviderInvoiceCreateRequest request)
    {
        var xtrfRequest = new XtrfRequest("/accounting/providers/invoices", Method.Post, Creds)
            .WithJsonBody(new
            {
                jobsIds = new []
                {
                    int.Parse(request.JobId)
                }
            });
        
        var response = await Client.ExecuteWithErrorHandling<CreateInvoiceEntity>(xtrfRequest);
        return await GetProviderInvoiceAsync(new ProviderInvoiceIdentifier { ProviderInvoiceId = response.InvoicesIds.First().ToString() });
    }
    
    [Action("Update provider invoice", Description = "Update provider invoice by ID")]
    public async Task<ProviderInvoiceResponse> UpdateProviderInvoiceAsync([ActionParameter] ProviderInvoiceUpdateRequest request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/providers/invoices/{request.ProviderInvoiceId}/status",
                Method.Post, Creds)
            .WithJsonBody(new
            {
                status = request.Status
            });
        
        await Client.ExecuteWithErrorHandling(xtrfRequest);
        return await GetProviderInvoiceAsync(request);
    }
    
    [Action("Send provider invoice", Description = "Send provider invoice by specified ID")]
    public async Task SendProviderInvoiceAsync([ActionParameter] ProviderInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/providers/invoices/{request.ProviderInvoiceId}/send", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(xtrfRequest);
    }
    
    [Action("Download provider invoice", Description = "Generates provider invoice document (PDF)")]
    public async Task<FileReference> DownloadProviderInvoiceAsync([ActionParameter] ProviderInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/providers/invoices/{request.ProviderInvoiceId}/document", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<UrlEntity>(xtrfRequest);
        
        var restClient = new RestClient(response.Url);
        var downloadResponse = await restClient.ExecuteAsync(new RestRequest(string.Empty));
        var rawBytes = downloadResponse.RawBytes!;
        
        var stream = new MemoryStream(rawBytes);
        stream.Position = 0;
        
        var fileReference = await fileManagementClient.UploadAsync(stream, "application/pdf", $"{request.ProviderInvoiceId}.pdf");
        return fileReference;
    }
    
    [Action("Delete provider invoice", Description = "Delete provider invoice by ID")]
    public async Task DeleteProviderInvoiceAsync([ActionParameter] ProviderInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/providers/invoices/{request.ProviderInvoiceId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(xtrfRequest);
    }
}