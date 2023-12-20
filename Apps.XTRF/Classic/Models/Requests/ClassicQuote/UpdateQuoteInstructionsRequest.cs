using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Requests.ClassicQuote;

public class UpdateQuoteInstructionsRequest
{
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