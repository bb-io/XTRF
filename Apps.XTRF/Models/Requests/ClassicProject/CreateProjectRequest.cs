using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Requests.ClassicProject;

public class CreateProjectRequest
{
    public string Name { get; set; }
 
    [Display("Customer")] 
    [DataSource(typeof(CustomerDataHandler))]
    public string CustomerId { get; set; }
    
    [Display("Service")] 
    [DataSource(typeof(ServiceDataSourceHandler))]
    public string ServiceId { get; set; }
        
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }
    
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? SourceLanguageId { get; set; }
    
    [Display("Target languages")]
    [DataSource(typeof(LanguageDataHandler))]
    public IEnumerable<string>? TargetLanguages { get; set; }
    
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