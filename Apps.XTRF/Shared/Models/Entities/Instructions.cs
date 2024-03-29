﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Entities;

public class Instructions
{
    [Display("Instruction from customer")]
    public string? FromCustomer { get; set; }
    
    [Display("Instruction for provider")]
    public string? ForProvider { get; set; }
    
    [Display("Internal instruction")]
    public string? Internal { get; set; }
    
    [Display("Payment note for customer")]
    public string? PaymentNoteForCustomer { get; set; }
    
    [Display("Payment note for vendor")]
    public string? PaymentNoteForVendor { get; set; }
    
    public string? Notes { get; set; }
}