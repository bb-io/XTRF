using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Requests
{
    public class UploadFileToQuoteRequest
    {
        [Display("Quote ID")]
        public string QuoteId { get; set; }

        public byte[] File { get; set; }

        [Display("File name")]
        public string FileName { get; set; }

        public string Category { get; set; }    
    }
}
