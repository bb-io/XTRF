using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Requests.ClassicTask;

public class CreateTaskRequest
{
    public string Name { get; set; }

    [Display("Specialization")]
    [DataSource(typeof(SpecializationDataHandler))]
    public string SpecializationId { get; set; }
    
    [Display("Workflow ID")]
    public string WorkflowId { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    [Display("Instruction from customer")]
    public string? InstructionFromCustomer { get; set; }
    
    [Display("Instruction for provider")]
    public string? InstructionForProvider { get; set; }
    
    [Display("Internal instruction")]
    public string? InternalInstruction { get; set; }
    
    [Display("Payment note for customer")]
    public string? PaymentNoteForCustomer { get; set; }
    
    public string? Notes { get; set; }
}