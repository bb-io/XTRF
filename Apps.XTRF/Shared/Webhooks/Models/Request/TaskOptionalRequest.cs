using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Webhooks.Models.Request;

public class TaskOptionalRequest
{
    [Display("Task ID")]
    public string? TaskId { get; set; }
}