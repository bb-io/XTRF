﻿using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Requests.ClassicJob;

public class UpdateJobRequest
{
    [StaticDataSource(typeof(StaticJobStatusDataHandler))]
    public string? Status { get; set; }
    
    [Display("Vendor ID")]
    public string? VendorId { get; set; }
    
    [Display("Start date")]
    public DateTime? StartDate { get; set; }
    
    public DateTime? Deadline { get; set; }

    [Display("Instruction from client")]
    public string? InstructionFromClient { get; set; }
    
    [Display("Instruction for vendor")]
    public string? InstructionForVendor { get; set; }
    
    [Display("Internal instruction")]
    public string? InternalInstruction { get; set; }

    [Display("Payment note for vendor")]
    public string? PaymentNoteForVendor { get; set; }
}