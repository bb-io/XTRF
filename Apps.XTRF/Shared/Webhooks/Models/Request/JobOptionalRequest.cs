using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Webhooks.Models.Request;

public class JobOptionalRequest
{
    [Display("Job ID")]
    public string? JobId { get; set; }

    [Display("Job type name")]
    [DataSource(typeof(JobTypeNameDataHandler))]
    public string? JobTypeName { get; set; }
}