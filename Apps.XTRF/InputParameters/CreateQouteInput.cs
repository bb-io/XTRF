using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models
{
    public class CreateQouteInput
    {
        public string Name { get; set; }
        
        [Display("Client ID")]
        public string ClientId { get; set; }
        
        [Display("Service ID")]
        public string ServiceId { get; set; }
        
        [Display("Opportunity offer ID")]
        public string OpportunityOfferId { get; set; }
    }
}
