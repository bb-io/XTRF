using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Invoice;

public class CustomerInvoiceSearchRequest
{
    [Display("Updated since")]
    public DateTime? UpdatedSince { get; set; }
}