namespace Apps.XTRF.Models.Responses.Entities;

public class ClassicProjectTask
{
    public string Id { get; set; }
    public string IdNumber { get; set; }
    public string ProjectId { get; set; }
    public string QuoteId { get; set; }
    public string Name { get; set; }
    public LanguageCombination LanguageCombination { get; set; }
    public ClassicDates Dates { get; set; }
    public ClassicProjectJobs Jobs { get; set; }
    public Instructions Instructions { get; set; }
    public IEnumerable<CustomField> CustomFields { get; set; }
}

public class ClassicProjectJobs 
{
    public IEnumerable<string> JobIds { get; set; }
}