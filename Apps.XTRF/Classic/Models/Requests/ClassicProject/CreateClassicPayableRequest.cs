using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XTRF.Classic.Models.Requests.ClassicProject;

public class CreateClassicPayableRequest : ClassicFinanceRequestBase
{
    [Display("Payable ID")]
    public string? Id { get; set; }
    
    [Display("Job ID", Description = "Job ID associated with the payable. You can locate it in the job section. Click on the job and check the URL for '&id=270'.")]    
    public string JobId { get; set; }

    [Display("File")]
    public FileReference File { get; set; }
}