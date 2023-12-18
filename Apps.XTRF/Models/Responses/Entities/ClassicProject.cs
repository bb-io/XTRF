namespace Apps.XTRF.Models.Responses.Entities;

public class ClassicProject
{
    public string Id { get; set; }
    public bool IsClassicProject { get; set; }
    public string IdNumber { get; set; }
    public string Name { get; set; }
    public string CustomerId { get; set; }
    public string ContactPersonId { get; set; }
    public string ProjectManagerId { get; set; }
    public string Status { get; set; }
    public string SpecializationId { get; set; }
    public ClassicDates Dates { get; set; }
    public Instructions Instructions { get; set; }
    public FinanceInformation Finance { get; set; }
    public ProjectContacts Contacts { get; set; }
    public IEnumerable<string> CategoriesIds { get; set; }
    public IEnumerable<ClassicProjectTask>? Tasks { get; set; }
    public IEnumerable<CustomField> CustomFields { get; set; }
}