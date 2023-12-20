using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Customer;

public class CreateContactInput
{
    public string Name { get; set; }
        
    [Display("Last name")]
    public string LastName { get; set; }
    public string Email { get; set; }
}