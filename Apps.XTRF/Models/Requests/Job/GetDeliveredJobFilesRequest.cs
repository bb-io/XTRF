using Apps.XTRF.DataSourceHandlers;
using Apps.XTRF.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.Job;

public class GetDeliveredJobFilesRequest
{
    [Display("Job ID")]
    public string JobId { get; set; }
    
    [Display("Filter language")] 
    [DataSource(typeof(LanguageDataHandler))]
    public string? LanguageId { get; set; }
    
    [DataSource(typeof(FileCategoryDataHandler))]
    public string? Category { get; set; } 
}