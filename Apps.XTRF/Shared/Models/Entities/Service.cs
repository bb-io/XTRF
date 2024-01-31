namespace Apps.XTRF.Shared.Models.Entities;

public class Service
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public bool Preferred { get; set; }
    public bool Classic { get; set; }
    public bool Default { get; set; }
}