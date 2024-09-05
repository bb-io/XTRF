namespace Apps.XTRF.Shared.Models.Responses.Invoices;

public class CustomerInvoiceSearchResponse
{
    public List<CustomerInvoiceResponse> Invoices { get; set; } = new();
}