using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Requests.Job;

public class ShareFileWithJobRequest
{
    [Display("Job ID")] public string JobId { get; set; }
    [Display("File ID")] public string FileId { get; set; }
}