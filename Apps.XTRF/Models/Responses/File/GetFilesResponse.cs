using Apps.XTRF.Models.Responses.Entities;

namespace Apps.XTRF.Models.Responses.File;

public class GetFilesResponse
{
    public IEnumerable<FileXTRF> Files { get; set; }
}