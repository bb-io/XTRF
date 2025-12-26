using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XTRF.Classic.Models.Requests.ClassicProject;

public class CreateClassicFinanceRequest : ClassicFinanceRequestBase
{
    [Display("File")]
    public FileReference File { get; set; }
}
