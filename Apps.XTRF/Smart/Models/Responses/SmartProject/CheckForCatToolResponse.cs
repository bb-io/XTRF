using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.SmartProject;

public class CheckForCatToolResponse
{
    [Display("Is project created in CAT tool or creation is queued")]
    public bool ProjectCreatedInCatToolOrCreationIsQueued { get; set; }
}