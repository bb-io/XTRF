using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Entities;

public class LanguageCombination
{
    [Display("Source language")]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language")]
    public string TargetLanguageId { get; set; }
}