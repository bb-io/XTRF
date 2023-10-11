using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Entities;

public class SharedFileStatus
{
    [Display("File ID")] public string FileId { get; set; }
    public bool Successful { get; set; }
    public string? Message { get; set; }
}