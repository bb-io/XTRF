using Apps.XTRF.Models.Requests.ManageCustomer;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Requests.Customer;

public class CreateContactInput : CustomerRequest
{
    public string Name { get; set; }
        
    [Display("Last name")]
    public string LastName { get; set; }
    public string Email { get; set; }
}