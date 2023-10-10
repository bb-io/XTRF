using Apps.XTRF.Models.Responses.Models;

namespace Apps.XTRF.Models.Responses;

public class SharedFilesResponse
{
    public IEnumerable<SharedFileStatus> Statuses { get; set; }
}