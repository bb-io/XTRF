using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Requests.Project;

public class UpdateProjectTargetLanguagesRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
    
    [Display("Target language IDs")]
    public IEnumerable<string> TargetLanguageIds { get; set; }
}