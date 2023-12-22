using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.SmartJob;

public class JobResponse
{
    public JobResponse(Entities.SmartJob job)
    {
        Id = job.Id;
        IdNumber = job.IdNumber;
        Name = job.Name;
        Status = job.Status;
        StepNumber = job.StepNumber;
        VendorId = job.VendorId;
        PurchaseOrderStatus = job.Documents.PurchaseOrderStatus;
        StartDate = job.Dates.StartDate?.ConvertFromUnixTime();
        Deadline = job.Dates.Deadline?.ConvertFromUnixTime();
        ActualStartDate = job.Dates.ActualStartDate?.ConvertFromUnixTime();
        ActualEndDate = job.Dates.ActualEndDate?.ConvertFromUnixTime();
        StepType = job.StepType;
        Files = job.Files;
        Instructions = job.Communication;
        Languages = job.Languages;
    }
    
    [Display("Job ID")] 
    public string Id { get; set; }
    
    [Display("ID number")]
    public string IdNumber { get; set; }
    
    public string Name { get; set; }
    
    public string Status { get; set; }
    
    [Display("Step number")] 
    public int StepNumber { get; set; }

    [Display("Vendor ID")] 
    public string VendorId { get; set; }
    
    [Display("Purchase order status")]
    public string PurchaseOrderStatus { get; set; }
    
    [Display("Start date")]
    public DateTime? StartDate { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    [Display("Actual start date")]
    public DateTime? ActualStartDate { get; set; }
    
    [Display("Actual end date")]
    public DateTime? ActualEndDate { get; set; }

    [Display("Step type")] 
    public StepType StepType { get; set; }

    public SmartJobFiles Files { get; set; }
    
    public SmartJobInstructions Instructions { get; set; }

    public IEnumerable<SmartJobLanguageCombination> Languages { get; set; }
}