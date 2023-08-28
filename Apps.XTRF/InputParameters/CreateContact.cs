using Apps.XTRF.Requests.ManageCustomer;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.InputParameters;

public class CreateContact : CustomerRequest
{
    public string Name { get; set; }
        
    [Display("Last name")]
    public string LastName { get; set; }
    public string Email { get; set; }
}