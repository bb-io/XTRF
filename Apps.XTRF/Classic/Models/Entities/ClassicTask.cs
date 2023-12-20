using Apps.XTRF.Shared.Models.Entities;

namespace Apps.XTRF.Classic.Models.Entities;

public class ClassicTask
{
    public string Id { get; set; }
    public string IdNumber { get; set; }
    public string ProjectId { get; set; }
    public string QuoteId { get; set; }
    public string Name { get; set; }
    public string ClientTaskPONumber { get; set; }
    public LanguageCombination LanguageCombination { get; set; }
    public ClassicDates Dates { get; set; }
    public ClassicTaskJobs Jobs { get; set; }
    public Instructions Instructions { get; set; }
    public ClassicTaskPeople People { get; set; }
    public IEnumerable<CustomField> CustomFields { get; set; }
}

public class ClassicTaskJobs 
{
    public IEnumerable<string> JobIds { get; set; }
}

public class ClassicTaskPeople 
{
    public ProjectContacts CustomerContacts { get; set; }
}