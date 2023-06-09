﻿using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses;

public class ProjectResponse
{
    public ProjectResponse(Project project)
    {
        Id = project.Id;
        IsClassicProject = project.IsClassicProject;
        QuoteIdNumber = project.QuoteIdNumber;
        Name = project.Name;
        Status = project.Status;
        BudgetCode = project.BudgetCode;
        ClientId = project.ClientId;
        ServiceId = project.ServiceId;
        Origin = project.Origin;
        ClientDeadline = project.ClientDeadline;
        ClientReferenceNumber = project.ClientReferenceNumber;
        ClientNotes = project.ClientNotes;
        InternalNotes = project.InternalNotes;
        ProjectIdNumber = project.ProjectIdNumber;
        InstructionsForAllJobs = project.InstructionsForAllJobs;
        OrderedOnUnix = project.OrderedOn;
        OrderedOn = project.OrderedOn is not null ? DateTimeOffset.FromUnixTimeSeconds((long)project.OrderedOn) : null;
    }

    public string? Id { get; set; }
    [Display("Is classic object?")] public bool? IsClassicProject { get; set; }
    [Display("Qoute id number")] public string? QuoteIdNumber { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    [Display("Budget code")] public string? BudgetCode { get; set; }
    [Display("Client id")] public int? ClientId { get; set; }
    [Display("Service id")] public int? ServiceId { get; set; }
    public string? Origin { get; set; }
    [Display("Client deadline")] public long? ClientDeadline { get; set; }
    [Display("Client reference number")] public string? ClientReferenceNumber { get; set; }
    [Display("Client notes")] public string? ClientNotes { get; set; }
    [Display("Internal notes")] public string? InternalNotes { get; set; }
    [Display("Project id number")] public string? ProjectIdNumber { get; set; }
    [Display("Instructions for all jobs")] public string? InstructionsForAllJobs { get; set; }
    [Display("Ordered on in UNIX")] public long? OrderedOnUnix { get; set; }
    [Display("Ordered on")] public DateTimeOffset? OrderedOn { get; set; }
}