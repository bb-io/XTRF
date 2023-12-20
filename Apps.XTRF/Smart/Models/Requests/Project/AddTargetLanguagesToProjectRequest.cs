using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.Project;

public class AddTargetLanguagesToProjectRequest
{
    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguageId { get; set; }
}