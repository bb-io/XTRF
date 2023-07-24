using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.InputParameters;

public class UpdateCustomer
{
    [Display("Customer ID")]
    public string Id { get; set; }
    public string? Name { get; set; }
        
    [Display("Full name")]
    public string? FullName { get; set; }
    public string? Email { get; set; }
}