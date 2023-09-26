using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models;

public class LanguageCombination
{
    [Display("Source language ID")]
    public int SourceLanguageId { get; set; }
    
    [Display("Target language ID")]
    public int TargetLanguageId { get; set; }
}