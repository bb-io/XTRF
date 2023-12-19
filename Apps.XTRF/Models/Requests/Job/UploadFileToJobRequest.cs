using Apps.XTRF.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.Job;

public class UploadFileToJobRequest : FileWrapper
{
    [DataSource(typeof(FileCategoryDataHandler))]
    public string Category { get; set; }    
    
    [Display("Filename")]
    public string? FileName { get; set; }
}