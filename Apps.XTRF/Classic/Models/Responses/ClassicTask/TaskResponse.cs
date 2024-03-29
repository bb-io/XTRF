﻿using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Responses.ClassicTask;

public class TaskResponse
{
    public TaskResponse(Entities.ClassicTask task, XtrfTimeZoneInfo timeZoneInfo)
    {
        Id = task.Id;
        IdNumber = task.IdNumber;
        ProjectId = task.ProjectId;
        QuoteId = task.QuoteId;
        Name = task.Name;
        ClientTaskPONumber = task.ClientTaskPONumber;
        LanguageCombination = task.LanguageCombination;
        StartDate = task.Dates.StartDate?.Time?.ConvertFromUnixTime(timeZoneInfo);
        Deadline = task.Dates.Deadline?.Time?.ConvertFromUnixTime(timeZoneInfo);
        ActualStartDate = task.Dates.ActualStartDate?.Time?.ConvertFromUnixTime(timeZoneInfo);
        ActualDeliveryDate = task.Dates.ActualDeliveryDate?.Time?.ConvertFromUnixTime(timeZoneInfo);
        JobIds = task.Jobs.JobIds;
        Instructions = task.Instructions;
        CustomerContacts = task.People.CustomerContacts;
    }
        
    [Display("Task ID")]
    public string Id { get; set; }
    
    [Display("ID number")]
    public string IdNumber { get; set; }
    
    [Display("Project ID")]
    public string ProjectId { get; set; }
    
    [Display("Quote ID")]
    public string QuoteId { get; set; }
    
    public string Name { get; set; }
    
    [Display("Client task PO number")]
    public string ClientTaskPONumber { get; set; }
    
    [Display("Language combination")]
    public LanguageCombination LanguageCombination { get; set; }
    
    [Display("Start date")]
    public DateTime? StartDate { get; set; }

    public DateTime? Deadline { get; set; }
    
    [Display("Actual start date")]
    public DateTime? ActualStartDate { get; set; }
    
    [Display("Actual delivery date")]
    public DateTime? ActualDeliveryDate { get; set; }
    
    [Display("Job IDs")]
    public IEnumerable<string> JobIds { get; set; }
    
    public Instructions Instructions { get; set; }
    
    [Display("Customer contacts")]
    public ClassicProjectContacts CustomerContacts { get; set; }
}