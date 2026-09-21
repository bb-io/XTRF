using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Webhooks.Models.Inputs;

public class ProjectNameContainsInput
{
    [Display("Project name contains", Description = "Only trigger for projects whose name contains the specified text")]
    public string? ProjectNameContains { get; set; }
}
