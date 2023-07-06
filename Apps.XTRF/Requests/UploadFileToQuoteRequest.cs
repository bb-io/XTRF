namespace Apps.XTRF.Requests
{
    public class UploadFileToQuoteRequest
    {
        public string QuoteId { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string Category { get; set; }    
    }
}
