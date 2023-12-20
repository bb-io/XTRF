using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Smart.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.Job;

public class GetDeliveredJobFilesRequest
{
    [Display("Filter language")] 
    [DataSource(typeof(LanguageDataHandler))]
    public string? LanguageId { get; set; }
    
    [Display("File category")]
    [DataSource(typeof(FileCategoryDataHandler))]
    public string? Category { get; set; } 
}