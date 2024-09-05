using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Invoice;

public class ClientInvoiceDuplicateRequest : CustomerInvoiceIdentifier
{
    [Display("Duplicate as pro forma")]
    public bool? DuplicateAsProForma { get; set; }
}