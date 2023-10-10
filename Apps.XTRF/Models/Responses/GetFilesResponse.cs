using Apps.XTRF.Models.Responses.Models;

namespace Apps.XTRF.Models.Responses;

public class GetFilesResponse
{
    public IEnumerable<FileXTRF> Files { get; set; }
}