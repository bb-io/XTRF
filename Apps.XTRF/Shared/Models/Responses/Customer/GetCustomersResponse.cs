using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Customer;

public class GetCustomersResponse
{
    [Display("Customers")]
    public IEnumerable<SimpleCustomer> SimpleCustomers { get; set; }
}