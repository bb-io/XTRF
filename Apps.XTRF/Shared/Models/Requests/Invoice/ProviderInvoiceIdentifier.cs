using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Requests.Invoice;

public class ProviderInvoiceIdentifier
{
    [Display("Provider invoice ID"), DataSource(typeof(ProviderInvoiceDataHandler))]
    public string ProviderInvoiceId { get; set; } = string.Empty;
}
