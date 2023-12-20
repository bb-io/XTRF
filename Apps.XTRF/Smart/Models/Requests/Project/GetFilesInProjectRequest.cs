using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Smart.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.Project;

public class GetFilesInProjectRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
    
    [Display("Filter language")] 
    [DataSource(typeof(LanguageDataHandler))]
    public string? LanguageId { get; set; }
    
    [DataSource(typeof(FileCategoryDataHandler))]
    public string? Category { get; set; }
}