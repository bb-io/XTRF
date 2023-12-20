namespace Apps.XTRF.Shared.Models.Entities;

public class Languages
{
    public int? SourceLanguageId { get; set; }
    
    public IEnumerable<string>? TargetLanguageIds { get; set; }
}