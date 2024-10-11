using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Webhooks.Models.Request;

public class ProjectOptionalRequest
{
    [Display("Project ID")]
    public string? ProjectId { get; set; }
}