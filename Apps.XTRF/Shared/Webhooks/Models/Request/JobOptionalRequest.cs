using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Webhooks.Models.Request;

public class JobOptionalRequest
{
    [Display("Job ID")]
    public string? JobId { get; set; }
}