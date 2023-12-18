using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Identifiers;

public class ClassicTaskIdentifier
{
    [Display("Task")]
    [DataSource(typeof(ClassicTaskDataHandler))]
    public string TaskId { get; set; }
}