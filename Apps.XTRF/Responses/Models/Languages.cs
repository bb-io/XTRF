namespace Apps.XTRF.Responses.Models;

public class Languages
{
    public int? SourceLanguageId { get; set; }
    
    public IEnumerable<int>? TargetLanguageIds { get; set; }
}