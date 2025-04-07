using Apps.XTRF.Classic.Models.Responses.ClassicTask;
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
    
    [PollingEvent("On client invoices status changed",
        "Triggered when status of any client invoice has changed.")]
    public async Task<PollingEventResponse<StatusMemory, CustomerInvoiceSearchResponse>> OnClientInvoicesStatusChanged(
        PollingEventRequest<StatusMemory> request, [PollingEventParameter] InvoiceStatusChangedInput input)
    {
        var customerInvoices = await GetCustomerInvoicesAsync(new());
        var statusMap = customerInvoices.Invoices.Where(x => x !=null).ToDictionary(x => x.Id, x => x.Status);
        
        if (request.Memory is null)
        {
            return new()
            {
                FlyBird = false,
                Memory = new()
                {
                    StatusMap = statusMap
                }
            };
        }

        var changedInvoices = customerInvoices.Invoices
            .Where(x =>
                request.Memory.StatusMap.Where(y => y.Key == x.Id).Select(x => x.Value).FirstOrDefault() != x.Status)
            .Where(x => input.Status is null || x.Status == input.Status)
            .Where(x => input.InvoiceId is null || x.Id == input.InvoiceId)
            .ToList();

        var invoicesWithDetails = new List<CustomerInvoiceResponse>();
        if (changedInvoices.Any())
        {
            foreach (var item in changedInvoices)
            {
                var xtrfRequest = new XtrfRequest($"/accounting/customers/invoices/{item.Id}?embed=tasks",
            Method.Get, Creds);
                var invoice = await Client.ExecuteWithErrorHandling<CustomerInvoiceResponse>(xtrfRequest);

                if (invoice == null)
                    continue;

                var timeZoneInfo = await GetTimeZoneInfo();
                invoice.TasksResponses = invoice.TasksDto?.Select(t => new TaskResponse(t, timeZoneInfo)).ToList() ?? new List<TaskResponse>();

                var paymentRequest = new XtrfRequest($"/accounting/customers/invoices/{item.Id}/payments",
                    Method.Get, Creds);
                var payments = await Client.ExecuteWithErrorHandling<List<PaymentResponse>>(paymentRequest);
                invoice.Payments = payments ?? new List<PaymentResponse>();

                invoice.TaskNames = invoice.TasksDto?.Select(x => x.Name).ToList() ?? new List<string>();
                invoicesWithDetails.Add(invoice);
            }
        }

        return new()
        {
            FlyBird = changedInvoices.Any(),
            Result = new()
            {
                Invoices = invoicesWithDetails.Any() ? invoicesWithDetails : changedInvoices
            },
            Memory = new()
            {
                StatusMap = statusMap
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

    [PollingEvent("On vendor invoices status changed",
    "Triggered when status of any vendor invoice has changed. Checks for vendor invoices with a specific status if provided.")]
    public async Task<PollingEventResponse<StatusMemory, ProviderInvoiceSearchResponse>> OnVendorInvoicesStatusChanged(
    PollingEventRequest<StatusMemory> request, [PollingEventParameter] VendorInvoiceStatusChangedInput input)
    {
        var vendorInvoices = await GetVendorInvoicesAsync(new());
        var statusMap = vendorInvoices.Invoices.ToDictionary(x => x.Id, x => x.Status);

        if (request.Memory is null)
        {
            return new PollingEventResponse<StatusMemory, ProviderInvoiceSearchResponse>
            {
                FlyBird = false,
                Memory = new StatusMemory
                {
                    StatusMap = statusMap
                }
            };
        }

        var changedInvoices = vendorInvoices.Invoices
            .Where(x =>
                request.Memory.StatusMap.TryGetValue(x.Id, out var previousStatus) ? previousStatus != x.Status : true)
            .Where(x => input.Status is null || x.Status == input.Status)
            .Where(x => input.InvoiceId is null || x.Id == input.InvoiceId)
            .ToList();

        var invoicesWithDetails = new List<ProviderInvoiceResponse>();
        if (changedInvoices.Any())
        {
            foreach (var item in changedInvoices)
            {
                var detailsRequest = new XtrfRequest($"/accounting/providers/invoices/{item.Id}", Method.Get, Creds);
                var invoice = await Client.ExecuteWithErrorHandling<ProviderInvoiceResponse>(detailsRequest);

                var paymentRequest = new XtrfRequest($"/accounting/providers/invoices/{item.Id}/payments", Method.Get, Creds);
                var payments = await Client.ExecuteWithErrorHandling<List<PaymentResponse>>(paymentRequest);
                invoice.Payments = payments;

                invoicesWithDetails.Add(invoice);
            }
        }

        return new PollingEventResponse<StatusMemory, ProviderInvoiceSearchResponse>
        {
            FlyBird = changedInvoices.Any(),
            Result = new ProviderInvoiceSearchResponse
            {
                Invoices = invoicesWithDetails.Any() ? invoicesWithDetails : changedInvoices
            },
            Memory = new StatusMemory
            {
                StatusMap = statusMap
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