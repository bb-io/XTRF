using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Requests.Project;

public class AddTargetLanguagesToProjectRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
    
    [Display("Target language ID")]
    public int TargetLanguageId { get; set; }
}