using Apps.XTRF.Responses.Models;

namespace Apps.XTRF.Responses;

public class SharedFilesResponse
{
    public IEnumerable<SharedFileStatus> Statuses { get; set; }
}