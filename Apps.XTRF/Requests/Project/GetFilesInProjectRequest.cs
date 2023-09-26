using Apps.XTRF.DataSourceHandlers;
using Apps.XTRF.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Requests.Project;

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