using Apps.XTRF.Shared.Models;
using Apps.XTRF.Smart.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.Job;

public class UploadFileToJobRequest : FileWrapper
{
    [DataSource(typeof(FileCategoryDataHandler))]
    public string Category { get; set; }    
    
    [Display("Filename")]
    public string? FileName { get; set; }
}