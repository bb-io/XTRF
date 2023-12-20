namespace Apps.XTRF.Shared.Models.Entities;

public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? LastName { get; set; }
    public Contact Contact { get; set; }
}