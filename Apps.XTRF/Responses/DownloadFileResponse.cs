using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses
{
    public class DownloadFileResponse
    {
        [Display("File content")]  public byte[] FileContent { get; set; }
    }
}
