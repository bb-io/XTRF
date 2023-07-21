using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Requests
{
    public class UploadFileToJobRequest
    {
        [Display("Job ID")]
        public string JobId { get; set; }

        public byte[] File { get; set; }

        [Display("File name")]
        public string FileName { get; set; }

        public string Category { get; set; }    
    }
}
