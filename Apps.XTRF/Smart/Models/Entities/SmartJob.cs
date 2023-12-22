using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Entities;

public class SmartJob
{
    public string Id { get; set; }
    public string IdNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public int StepNumber { get; set; }
    public string VendorId { get; set; }
    public StepType StepType { get; set; }
    public JobDates Dates { get; set; }
    public SmartJobFiles Files { get; set; }
    public SmartJobInstructions Communication { get; set; }
    public JobDocuments Documents { get; set; }
    public IEnumerable<SmartJobLanguageCombination> Languages { get; set; }
}

public class StepType
{
    [Display("Step type ID")]
    public string Id { get; set; }
    
    [Display("Step type name")]
    public string Name { get; set; }
    
    [Display("Job type ID")]
    public string JobTypeId { get; set; }
}

public class SmartJobFiles
{
    [Display("Shared work files IDs")]
    public IEnumerable<string> SharedWorkFiles { get; set; }
    
    [Display("Shared reference files IDs")]
    public IEnumerable<string> SharedReferenceFiles { get; set; }
    
    [Display("Delivered in job files IDs")]
    public IEnumerable<string> DeliveredInJobFiles { get; set; }
}

public class SmartJobInstructions
{
    [Display("Instructions for all jobs")]
    public string InstructionsForAllJobs { get; set; }
    
    [Display("Instructions for job")]
    public string InstructionsForJob { get; set; }
    
    [Display("Note from vendor")]
    public string NoteFromVendor { get; set; }
}