using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Invoice;
using Apps.XTRF.Shared.Models.Responses.Invoices;
using Apps.XTRF.Shared.Polling.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.XTRF.Shared.Polling;

[PollingEventList]
public class InvoicePollingList(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
{
    [PollingEvent("On client invoices created",
        "Triggered when new client invoices are created. Checks for new invoices based on specified interval.")]
    public async Task<PollingEventResponse<DateMemory, CustomerInvoiceSearchResponse>> OnClientInvoicesCreated(
        PollingEventRequest<DateMemory> request)
    {
        if (request.Memory is null)
        {
            return new()
            {
                FlyBird = false,
                Memory = new()
                {
                    LastInteractionDate = DateTime.UtcNow
                }
            };
        }

        var customerInvoices = await GetCustomerInvoicesAsync(new()
        {
            UpdatedSince = request.Memory.LastInteractionDate!.Value
        });

        return new()
        {
            FlyBird = customerInvoices.Invoices.Any(),
            Result = customerInvoices,
            Memory = new()
            {
                LastInteractionDate = DateTime.UtcNow
            }
        };
    }
    
    [PollingEvent("On vendor invoices created",
        "Triggered when new vendor invoices are created. Checks for new invoices based on specified interval.")]
    public async Task<PollingEventResponse<DateMemory, ProviderInvoiceSearchResponse>> OnVendorInvoicesCreated(
        PollingEventRequest<DateMemory> request)
    {
        if (request.Memory is null)
        {
            return new()
            {
                FlyBird = false,
                Memory = new()
                {
                    LastInteractionDate = DateTime.UtcNow
                }
            };
        }

        var customerInvoices = await GetVendorInvoicesAsync(new()
        {
            UpdatedSince = request.Memory.LastInteractionDate
        });
        
        return new()
        {
            FlyBird = customerInvoices.Invoices.Any(),
            Result = customerInvoices,
            Memory = new()
            {
                LastInteractionDate = DateTime.UtcNow
            }
        };
    }

    private async Task<CustomerInvoiceSearchResponse> GetCustomerInvoicesAsync(CustomerInvoiceSearchRequest request)
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
    
    private async Task<ProviderInvoiceSearchResponse> GetVendorInvoicesAsync(CustomerInvoiceSearchRequest request)
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
}