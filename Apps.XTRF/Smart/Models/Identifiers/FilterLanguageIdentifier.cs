using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Identifiers;

public class FilterLanguageOptionalIdentifier
{
    [Display("Filter language")] 
    [DataSource(typeof(LanguageDataHandler))]
    public string? LanguageId { get; set; }
}