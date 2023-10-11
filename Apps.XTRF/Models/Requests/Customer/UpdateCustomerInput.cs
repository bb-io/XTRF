using Apps.XTRF.Models.Requests.ManageCustomer;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Requests.Customer;

public class UpdateCustomerInput : CustomerRequest
{
    public string? Name { get; set; }
        
    [Display("Full name")]
    public string? FullName { get; set; }
    public string? Email { get; set; }
}