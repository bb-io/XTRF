using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Identifiers;

public class CustomerInvoiceIdentifier
{
    [Display("Customer invoice ID"), DataSource(typeof(CustomerInvoiceDataSource))]
    public string CustomerInvoiceId { get; set; } = string.Empty;
}