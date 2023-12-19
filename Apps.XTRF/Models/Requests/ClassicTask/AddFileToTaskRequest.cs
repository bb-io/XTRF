using Apps.XTRF.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.ClassicTask;

public class AddFileToTaskRequest : FileWrapper
{
    [DataSource(typeof(ClassicFileCategoryDataHandler))]
    public string Category { get; set; }    
}