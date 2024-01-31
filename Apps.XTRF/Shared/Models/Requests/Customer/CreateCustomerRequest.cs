using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Customer;

public class CreateCustomerRequest
{
    public string Name { get; set; }
        
    [Display("Full name")]
    public string FullName { get; set; }
    
    [Display("Primary email")]
    public string Email { get; set; }
    
    public string? Notes { get; set; }
    
    public IEnumerable<string>? Phones { get; set; }
    
    [Display("Additional emails")]
    public IEnumerable<string>? AdditionalEmails { get; set; }
}