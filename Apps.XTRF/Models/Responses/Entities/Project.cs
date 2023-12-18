﻿namespace Apps.XTRF.Models.Responses.Entities;

public class Project
{
    public string Id { get; set; }
    public string ProjectId { get; set; }
    public bool? IsClassicProject { get; set; }
    public string? QuoteIdNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string? BudgetCode { get; set; }
    public string ClientId { get; set; }
    public string ServiceId { get; set; }
    public string? Origin { get; set; }
    public long? ClientDeadline { get; set; }
    public string? ClientReferenceNumber { get; set; }
    public string? ClientNotes { get; set; }
    public string? InternalNotes { get; set; }
    public string? ProjectIdNumber { get; set; }
    public string? InstructionsForAllJobs { get; set; }
    public long? OrderedOn { get; set; }
    public Languages Languages { get; set; }
}