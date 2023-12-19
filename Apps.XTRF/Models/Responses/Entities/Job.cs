using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Entities;

public class Job
{
    [Display("Job ID")] 
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Status { get; set; }
    
    [Display("Step number")] 
    public int StepNumber { get; set; }

    [Display("Vendor ID")] 
    public string? VendorId { get; set; }

    [Display("Step type")] 
    public StepType StepType { get; set; }
    
    public List<JobLanguage> Languages { get; set; }
}

public class JobLanguage
{
    [Display("Source language")]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language")]
    public string TargetLanguageId { get; set; }
    
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }
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