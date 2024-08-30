using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Entities;

public class Person
{
    [Display("Person ID")]
    public string Id { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    [Display(("Last name"))]
    public string LastName { get; set; } = string.Empty;
    
    public Contact Contact { get; set; } = new();
    
    [Display("Position ID")]
    public string PositionId { get; set; } = string.Empty;
}