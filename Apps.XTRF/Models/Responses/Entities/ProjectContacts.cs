using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Entities;

public class ProjectContacts
{
    [Display("Primary contact person")]
    public string PrimaryId { get; set; }
    
    [Display("Send back to contact person")]
    public string SendBackToId { get; set; }
        
    [Display("Additional contact persons")]  
    public IEnumerable<string> AdditionalIds { get; set; }
}