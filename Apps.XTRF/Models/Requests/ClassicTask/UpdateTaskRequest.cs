using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.ClassicTask;

public class UpdateTaskRequest
{
    public string? Name { get; set; }
    
    [Display("Client task PO number")]
    public string? ClientTaskPONumber { get; set; }
    
    [Display("Primary contact person")]
    [DataSource(typeof(ProjectContactDataHandler))]
    public string? PrimaryId { get; set; }
    
    [Display("Send back to contact person")]
    [DataSource(typeof(ProjectContactDataHandler))]
    public string? SendBackToId { get; set; }
        
    [Display("Additional contact persons")]  
    [DataSource(typeof(ProjectContactDataHandler))]
    public IEnumerable<string>? AdditionalIds { get; set; }
    
    [Display("Start date")]
    public DateTime? StartDate { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    [Display("Actual start date")]
    public DateTime? ActualStartDate { get; set; }
    
    [Display("Actual delivery date")]
    public DateTime? ActualDeliveryDate { get; set; }
    
    [Display("Instruction from customer")]
    public string? InstructionFromCustomer { get; set; }
    
    [Display("Instruction for provider")]
    public string? InstructionForProvider { get; set; }
    
    [Display("Internal instruction")]
    public string? InternalInstruction { get; set; }
    
    [Display("Payment note for customer")]
    public string? PaymentNoteForCustomer { get; set; }
    
    [Display("Payment note for vendor")]
    public string? PaymentNoteForVendor { get; set; }
    
    public string? Notes { get; set; }
}