using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.InputParameters
{
    public class SetPhoneNumber
    {
        [Display("Contact ID")]
        public string ContactId { get; set; }
        
        [Display("Phone number")]
        public string PhoneNumber { get; set; }
    }
}
