using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.Project;

public class CheckForCatToolResponse
{
    [Display("Project created int cat tool or creation is queued")]
    public bool ProjectCreatedInCatToolOrCreationIsQueued { get; set; }
}