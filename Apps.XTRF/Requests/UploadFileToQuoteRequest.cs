using Apps.XTRF.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.XTRF.Requests;

public class UploadFileToQuoteRequest
{
    [Display("Quote ID")]
    public string QuoteId { get; set; }

    public File File { get; set; }

    [Display("File name")]
    public string? FileName { get; set; }

    [DataSource(typeof(FileCategoryDataHandler))]
    public string Category { get; set; }    
}