namespace Apps.XTRF.Requests
{
    public class UploadFileToProjectRequest
    {
        public string ProjectId { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string Category { get; set; }    
    }
}
