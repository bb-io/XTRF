namespace Apps.XTRF.Models.Responses.Entities;

public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? LastName { get; set; }
    public Contact Contact { get; set; }
}