using Apps.XTRF.Responses.Models;

namespace Apps.XTRF.Responses
{
    public class GetFilesResponse
    {
        public IEnumerable<FileXTRF> Files { get; set; }
    }
}
