using Apps.XTRF.Shared.Models.Entities;

namespace Apps.XTRF.Classic.Models.Entities;

public class Person
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string LastName { get; set; }
    
    public Contact Contact { get; set; }
    
    public int? PositionId { get; set; }
}