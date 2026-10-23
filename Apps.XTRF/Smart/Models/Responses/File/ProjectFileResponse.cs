using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.File;

public class ProjectFileResponse : SmartFileXTRF
{
    [Display("URL")]
    public string? Url { get; set; }

    [Display("Is link")]
    public bool? IsLink { get; set; }
}
