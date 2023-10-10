using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.ManageCustomer;

public class CustomerRequest
{
    [Display("Customer")]
    [DataSource(typeof(CustomerDataHandler))]
    public string CustomerId { get; set; }
}