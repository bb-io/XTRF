namespace Apps.XTRF.Models.Responses.Entities;

public class Languages
{
    public int? SourceLanguageId { get; set; }
    
    public IEnumerable<string>? TargetLanguageIds { get; set; }
}