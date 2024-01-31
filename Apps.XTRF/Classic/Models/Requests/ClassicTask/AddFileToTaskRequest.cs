using Apps.XTRF.Classic.DataSourceHandlers.EnumHandlers;
using Apps.XTRF.Shared.Models;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Requests.ClassicTask;

public class AddFileToTaskRequest : FileWrapper
{
    [DataSource(typeof(ClassicFileCategoryDataHandler))]
    public string Category { get; set; }    
}