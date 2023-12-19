using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Entities;

public class ClassicShortJob
{
    [Display("Job ID")] 
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    [Display("ID number")]
    public string IdNumber { get; set; }
    
    public ClassicJobFiles Files { get; set; }
}

public class ClassicJob : ClassicShortJob
{
    public string Status { get; set; }
    public string SpecializationId { get; set; }
    public string? VendorId { get; set; }
    public ClassicJobStepType StepType { get; set; }
    public LanguageCombination Languages { get; set; }
    public ClassicJobDates Dates { get; set; }
    public ClassicJobInstructions Instructions { get; set; }
    public ClassicJobDocuments Documents { get; set; }
}

public class ClassicJobFiles
{
    public IEnumerable<ClassicFileXTRF> InputFiles { get; set; }
    public IEnumerable<ClassicFileXTRF> OutputFiles { get; set; }
}

public class ClassicJobStepType
{
    [Display("Job type")]
    public string JobType { get; set; }
    
    public string Name { get; set; }
    
    [Display("Job type ID")]
    public string JobTypeId { get; set; }
}

public class ClassicJobDates 
{
    public long? StartDate { get; set; }
    public long? Deadline { get; set; }
    public long? ActualStartDate { get; set; }
    public long? ActualEndDate { get; set; }
}

public class ClassicJobDocuments
{
    public string PurchaseOrderStatus { get; set; }
}

public class ClassicJobInstructions
{
    [Display("Instruction from client")]
    public string? FromClient { get; set; }
    
    [Display("Instruction for vendor")]
    public string? ForVendor { get; set; }
    
    [Display("Internal instruction")]
    public string? Internal { get; set; }

    [Display("Payment note for vendor")]
    public string? PaymentNoteForVendor { get; set; }
}