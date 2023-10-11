using Apps.XTRF.Models.Responses.Entities;

namespace Apps.XTRF.Models.Responses.Job;

public class SharedFilesResponse
{
    public IEnumerable<SharedFileStatus> Statuses { get; set; }
}