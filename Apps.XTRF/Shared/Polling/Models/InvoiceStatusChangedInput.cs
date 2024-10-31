using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Polling.Models;

public class InvoiceStatusChangedInput
{
    [Display("Customer invoice ID"), DataSource(typeof(CustomerInvoiceDataSource))]
    public string? InvoiceId { get; set; }

    [StaticDataSource(typeof(CustomerInvoiceStatusDataSource))]
    public string? Status { get; set; }
}