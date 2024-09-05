using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.Models;

public class CreateCustomerInvoiceRequest
{
    [Display("Invoice type"), StaticDataSource(typeof(CustomerInvoiceTypeDataSource))]
    public string InvoiceType { get; set; } = string.Empty;

    [Display("Task IDs")]
    public IEnumerable<string>? TaskIds { get; set; }

    [Display("Prepayment IDs")]
    public IEnumerable<string>? PrepaymentIds { get; set; }
}