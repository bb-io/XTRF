using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Identifiers;

public class CustomerIdentifier
{
    [Display("Customer")]
    [DataSource(typeof(CustomerDataHandler))]
    public string CustomerId { get; set; }
}