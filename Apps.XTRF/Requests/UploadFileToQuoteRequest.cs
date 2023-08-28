using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.XTRF.Requests;

public class UploadFileToQuoteRequest
{
    [Display("Quote ID")]
    public string QuoteId { get; set; }

    public File File { get; set; }

    [Display("File name")]
    public string? FileName { get; set; }

    public string Category { get; set; }    
}