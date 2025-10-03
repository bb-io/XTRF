using Apps.XTRF.Classic.Models.Entities;

namespace Apps.XTRF.Classic.Models.Responses.ClassicTask;

public class JobFilesResponse
{
    public IEnumerable<ClassicShortJob> Jobs { get; set; } = Enumerable.Empty<ClassicShortJob>();
    public IEnumerable<ClassicFileXTRF>? OutputFiles { get; set; }
}