using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Entities;

public class SmartProjectContacts
{
    [Display("Primary contact person")]
    public string? PrimaryId { get; set; }

    [Display("Additional contact persons")]  
    public IEnumerable<string>? AdditionalIds { get; set; }
}