using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Customer;

public class GetCustomersResponse
{
    [Display("Customers")]
    public IEnumerable<SimpleCustomer> SimpleCustomers { get; set; }
}