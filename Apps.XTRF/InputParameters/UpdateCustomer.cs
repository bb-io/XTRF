using Apps.XTRF.Requests.ManageCustomer;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.InputParameters;

public class UpdateCustomer : CustomerRequest
{
    public string? Name { get; set; }
        
    [Display("Full name")]
    public string? FullName { get; set; }
    public string? Email { get; set; }
}