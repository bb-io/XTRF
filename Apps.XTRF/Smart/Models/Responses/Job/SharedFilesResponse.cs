using Apps.XTRF.Smart.Models.Entities;

namespace Apps.XTRF.Smart.Models.Responses.Job;

public class SharedFilesResponse
{
    public IEnumerable<SharedFileStatus> Statuses { get; set; }
}