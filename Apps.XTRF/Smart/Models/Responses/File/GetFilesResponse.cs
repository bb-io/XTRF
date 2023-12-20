using Apps.XTRF.Smart.Models.Entities;

namespace Apps.XTRF.Smart.Models.Responses.File;

public class GetFilesResponse
{
    public IEnumerable<FileXTRF> Files { get; set; }
}