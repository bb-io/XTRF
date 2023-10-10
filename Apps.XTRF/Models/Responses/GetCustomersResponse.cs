using Apps.XTRF.Models.Responses.Models;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses;

public class GetCustomersResponse
{
    [Display("Simple customers")] public IEnumerable<SimpleCustomer> SimpleCustomers { get; set; }
}