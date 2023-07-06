namespace Apps.XTRF.Requests
{
    public class UploadFileToJobRequest
    {
        public string JobId { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string Category { get; set; }    
    }
}
