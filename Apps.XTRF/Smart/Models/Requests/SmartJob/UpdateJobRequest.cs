using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.SmartJob;

public class UpdateJobRequest
{
    [StaticDataSource(typeof(StaticJobStatusDataHandler))]
    public string? Status { get; set; }
    
    [Display("Vendor ID")]
    public string? VendorId { get; set; }
    
    [Display("Start date")]
    public DateTime? StartDate { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    public string? Instructions { get; set; }
}