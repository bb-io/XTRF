using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Identifiers;

public class LanguageCombinationIdentifier
{
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguageId { get; set; }
}