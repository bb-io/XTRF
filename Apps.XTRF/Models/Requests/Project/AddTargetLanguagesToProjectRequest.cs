using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.Project;

public class AddTargetLanguagesToProjectRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
    
    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguageId { get; set; }
}