using Apps.XTRF.Shared.Models.Identifiers;

namespace Apps.XTRF.Shared.Models.Requests.Invoice;

public class ExportCustomerInvoiceRequest : CustomerInvoiceIdentifier
{
    public string Currency { get; set; } = string.Empty;
}