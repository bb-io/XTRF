using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Customer;

public class CreateCustomerInput
{
    public string Name { get; set; }
        
    [Display("Full name")]
    public string FullName { get; set; }
    public string Email { get; set; }
}