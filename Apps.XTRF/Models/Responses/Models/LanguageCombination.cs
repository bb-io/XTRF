using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Models;

public class LanguageCombination
{
    [Display("Source language ID")]
    public string? SourceLanguageId { get; set; }
    
    [Display("Target language ID")]
    public string? TargetLanguageId { get; set; }
}