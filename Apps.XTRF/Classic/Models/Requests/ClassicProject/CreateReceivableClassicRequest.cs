using Apps.XTRF.Classic.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Requests.ClassicProject;

public class CreateReceivableClassicRequest : ClassicFinanceRequestBase
{
    [Display("File")]
    public FileReference? File { get; set; }

    [Display("Receivable ID")]
    public string? Id { get; set; }

    [Display("Task ID"), DataSource(typeof(ClassicTaskDataHandler))]
    public string TaskId { get; set; }
}
