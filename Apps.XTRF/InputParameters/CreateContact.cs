using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.InputParameters
{
    public class CreateContact
    {
        [Display("Customer ID")]
        public string CustomerId { get; set; }
        public string Name { get; set; }
        
        [Display("Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
