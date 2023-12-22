using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Entities;

public class ClassicProjectContacts
{
    [Display("Primary contact person")]
    public string? PrimaryId { get; set; }
    
    [Display("Send back to contact person")]
    public string? SendBackToId { get; set; }
        
    [Display("Additional contact persons")]  
    public IEnumerable<string>? AdditionalIds { get; set; }
}