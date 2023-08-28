using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses;

public class GetCustomersResponse
{
    [Display("Simple customers")] public IEnumerable<SimpleCustomer> SimpleCustomers { get; set; }
}