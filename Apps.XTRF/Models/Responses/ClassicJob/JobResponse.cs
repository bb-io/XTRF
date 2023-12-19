﻿using Apps.XTRF.Extensions;
using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.ClassicJob;

public class JobResponse
{
    public JobResponse(Entities.ClassicJob job)
    {
        Id = job.Id;
        Name = job.Name;
        IdNumber = job.IdNumber;
        Status = job.Status;
        SpecializationId = job.SpecializationId;
        VendorId = job.VendorId;
        PurchaseOrderStatus = job.Documents.PurchaseOrderStatus;
        StartDate = job.Dates.StartDate?.ConvertFromUnixTime();
        Deadline = job.Dates.Deadline?.ConvertFromUnixTime();
        ActualStartDate = job.Dates.ActualStartDate?.ConvertFromUnixTime();
        ActualEndDate = job.Dates.ActualEndDate?.ConvertFromUnixTime();
        StepType = job.StepType;
        Languages = job.Languages;
        Instructions = job.Instructions;
        InputFiles = job.Files.InputFiles;
        OutputFiles = job.Files.OutputFiles;
    }
    
    [Display("Job ID")] 
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    [Display("ID number")]
    public string IdNumber { get; set; }
    
    public string Status { get; set; }
    
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }
    
    [Display("Vendor ID")]
    public string? VendorId { get; set; }
    
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
    public ClassicJobStepType StepType { get; set; }
    
    public LanguageCombination Languages { get; set; }
    
    public ClassicJobInstructions Instructions { get; set; }
    
    [Display("input files")]
    public IEnumerable<ClassicFileXTRF> InputFiles { get; set; }
    
    [Display("Output files")]
    public IEnumerable<ClassicFileXTRF> OutputFiles { get; set; }
}