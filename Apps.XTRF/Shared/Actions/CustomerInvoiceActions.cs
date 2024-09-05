using System.Text;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Shared.Models.Requests.Invoice;
using Apps.XTRF.Shared.Models.Responses.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XTRF.Shared.Actions;

[ActionList]
public class CustomerInvoiceActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : XtrfInvocable(invocationContext)
{
    [Action("Search customer invoices", Description = "Search for customer invoices based on the given criteria")]
    public async Task<CustomerInvoiceSearchResponse> SearchCustomerInvoicesAsync([ActionParameter] CustomerInvoiceSearchRequest request)
    {
        var xtrfRequest = new XtrfRequest("/accounting/customers/invoices", Method.Get, Creds);
        if (request.UpdatedSince.HasValue)
        {
            var unixTimestamp = ((DateTimeOffset)request.UpdatedSince.Value).ToUnixTimeMilliseconds();
            xtrfRequest.AddQueryParameter("updatedSince", unixTimestamp.ToString());
        }
        
        var invoices = await Client.ExecuteWithErrorHandling<List<CustomerInvoiceResponse>>(xtrfRequest);
        return new()
        {
            Invoices = invoices
        };
    }
    
    [Action("Get customer invoice", Description = "Get customer invoice by ID")]
    public async Task<CustomerInvoiceResponse> GetCustomerInvoiceAsync([ActionParameter] CustomerInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/customers/invoices/{request.CustomerInvoiceId}", Method.Get, Creds);
        var invoice = await Client.ExecuteWithErrorHandling<CustomerInvoiceResponse>(xtrfRequest);
        
        var paymentRequest = new XtrfRequest($"/accounting/customers/invoices/{request.CustomerInvoiceId}/payments", Method.Get, Creds);
        var payments = await Client.ExecuteWithErrorHandling<List<PaymentResponse>>(paymentRequest);
        invoice.Payments = payments;
        
        return invoice;
    }
    
    [Action("Export customer invoice", Description = "Export customer invoice by ID to json file to be imported in external system")]
    public async Task<FileReference> ExportCustomerInvoiceAsync([ActionParameter] ExportCustomerInvoiceRequest request)
    {
        var invoice = await GetCustomerInvoiceAsync(request);
        var lines = invoice.Payments.Select(l => new
        {
            description = l.Notes ?? "Payment",
            quantity = 1, 
            unit_price = l.Amount,
            amount = l.Amount
        }).ToList();

        var taxesAmount = invoice.TotalGross - invoice.TotalNetto;
        var taxes = taxesAmount == 0 ? new List<object>() :
        [
            new
            {
                description = "Total taxes",
                amount = taxesAmount
            }
        ];
        
        var jsonInvoice = new List<object>
        {
            new
            {
                customer_name = invoice.CustomerDetails.Name,
                invoice_number = invoice.InvoiceNumber,
                invoice_date = invoice.Dates.FinalDate,
                currency = request.Currency,
                lines,
                sub_total = lines.Sum(l => l.amount),
                taxes,
                total = invoice.TotalGross,
            }
        };
        
        var json = new
        {
            invoices = jsonInvoice
        };
        
        var jsonString = JsonConvert.SerializeObject(json);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        stream.Position = 0;
        
        return await fileManagementClient.UploadAsync(stream, "application/json", $"{request.CustomerInvoiceId}.json");
    }
    
    [Action("Create customer invoice", Description = "Create customer invoice")]
    public async Task<CustomerInvoiceResponse> CreateCustomerInvoiceAsync([ActionParameter] CreateCustomerInvoiceRequest request)
    {
        var xtrfRequest = new XtrfRequest("/accounting/customers/invoices", Method.Post, Creds)
            .AddJsonBody(new
            {
                type = request.InvoiceType,
                tasksIds = request.TaskIds ?? new List<string>(),
                prepaymentIds = request.PrepaymentIds ?? new List<string>()
            });
        
        return await Client.ExecuteWithErrorHandling<CustomerInvoiceResponse>(xtrfRequest);
    }
    
    [Action("Delete customer invoice", Description = "Delete customer invoice by ID")]
    public async Task DeleteCustomerInvoiceAsync([ActionParameter] CustomerInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/customers/invoices/{request.CustomerInvoiceId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(xtrfRequest);
    }
    
    [Action("Download customer invoice", Description = "Download customer invoice by ID")]
    public async Task<FileReference> DownloadCustomerInvoiceAsync([ActionParameter] CustomerInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/customers/invoices/{request.CustomerInvoiceId}/document", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<UrlEntity>(xtrfRequest);
        
        var restClient = new RestClient(response.Url);
        var downloadResponse = await restClient.ExecuteAsync(new RestRequest(string.Empty));
        var rawBytes = downloadResponse.RawBytes!;
        
        var stream = new MemoryStream(rawBytes);
        stream.Position = 0;
        
        return await fileManagementClient.UploadAsync(stream, "application/pdf", $"{request.CustomerInvoiceId}.pdf");
    }
    
    [Action("Send reminder for customer invoice", Description = "Send reminder for customer invoice by ID")]
    public async Task SendReminderForCustomerInvoiceAsync([ActionParameter] CustomerInvoiceIdentifier request)
    {
        var xtrfRequest = new XtrfRequest($"/accounting/customers/invoices/{request.CustomerInvoiceId}/sendReminder", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(xtrfRequest);
    }
    
    [Action("Duplicate customer invoice", Description = "Duplicate customer invoice by ID")]
    public async Task<CustomerInvoiceResponse> DuplicateCustomerInvoiceAsync([ActionParameter] ClientInvoiceDuplicateRequest request)
    {
        var endpoint = $"/accounting/customers/invoices/{request.CustomerInvoiceId}/duplicate";
        endpoint += request.DuplicateAsProForma.HasValue && request.DuplicateAsProForma.Value ? "/proForma" : string.Empty;
        var xtrfRequest = new XtrfRequest(endpoint, Method.Post, Creds);
        return await Client.ExecuteWithErrorHandling<CustomerInvoiceResponse>(xtrfRequest);
    }
}