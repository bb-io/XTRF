using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models;

public class CustomerOptionalRequest
{
    [Display("Customer ID")]
    [DataSource(typeof(CustomerDataHandler))]
    public string? CustomerId { get; set; }
}
