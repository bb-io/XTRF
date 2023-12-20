using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.Project;

public class GetClientContactsByProjectResponse
{
    [Display("Primary contact person")]
    public string PrimaryId { get; set; }
    
    [Display("Send back to contact person")]
    public string SendBackToId { get; set; }
        
    [Display("Additional contact persons")]  
    public IEnumerable<string> AdditionalIds { get; set; }
}