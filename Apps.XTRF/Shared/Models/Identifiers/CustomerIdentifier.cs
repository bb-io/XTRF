using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Identifiers;

public class CustomerIdentifier
{
    [Display("Customer")]
    [DataSource(typeof(CustomerDataHandler))]
    public string CustomerId { get; set; }
}