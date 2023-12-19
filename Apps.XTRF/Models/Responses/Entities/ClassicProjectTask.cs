namespace Apps.XTRF.Models.Responses.Entities;

public class ClassicProjectTask
{
    public string Id { get; set; }
    public string IdNumber { get; set; }
    public string ProjectId { get; set; }
    public string QuoteId { get; set; }
    public string Name { get; set; }
    public string ClientTaskPONumber { get; set; }
    public LanguageCombination LanguageCombination { get; set; }
    public ClassicDates Dates { get; set; }
    public ClassicProjectTaskJobs Jobs { get; set; }
    public Instructions Instructions { get; set; }
    public ClassicProjectTaskPeople People { get; set; }
    public IEnumerable<CustomField> CustomFields { get; set; }
}

public class ClassicProjectTaskJobs 
{
    public IEnumerable<string> JobIds { get; set; }
}

public class ClassicProjectTaskPeople 
{
    public ProjectContacts CustomerContacts { get; set; }
}