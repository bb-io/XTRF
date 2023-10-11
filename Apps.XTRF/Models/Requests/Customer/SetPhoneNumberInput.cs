using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Requests.Customer;

public class SetPhoneNumberInput
{
    [Display("Contact ID")]
    public string ContactId { get; set; }
        
    [Display("Phone number")]
    public string PhoneNumber { get; set; }
}