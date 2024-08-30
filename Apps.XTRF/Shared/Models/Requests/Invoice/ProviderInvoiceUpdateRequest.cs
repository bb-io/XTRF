using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.Models.Requests.Invoice;

public class ProviderInvoiceUpdateRequest : ProviderInvoiceIdentifier
{
    [Display("Invoice status"), StaticDataSource(typeof(ProviderInvoiceStatusDataHandler))]
    public string Status { get; set; } = string.Empty;
}