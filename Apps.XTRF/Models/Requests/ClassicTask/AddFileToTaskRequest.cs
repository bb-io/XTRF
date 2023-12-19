using Apps.XTRF.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.XTRF.Models.Requests.ClassicTask;

public class AddFileToTaskRequest
{
    public File File { get; set; }

    [DataSource(typeof(ClassicFileCategoryDataHandler))]
    public string Category { get; set; }    
}