using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.SmartProject;

public class ProjectResponse
{
    public ProjectResponse(Entities.SmartProject project)
    {
        Id = project.Id;
        IsClassicProject = project.IsClassicProject;
        ProjectIdNumber = project.ProjectIdNumber;
        QuoteIdNumber = project.QuoteIdNumber;
        Name = project.Name;
        ClientId = project.ClientId;
        ServiceId = project.ServiceId;
        ProjectManagerId = project.People.ProjectManagerId;
        SpecializationId = project.Languages.SpecializationId;
        Status = project.Status;
        BudgetCode = project.BudgetCode;
        Origin = project.Origin;
        ClientReferenceNumber = project.ClientReferenceNumber;
        ClientNotes = project.ClientNotes;
        InternalNotes = project.InternalNotes;
        InstructionsForAllJobs = project.InstructionsForAllJobs;
        ClientDeadline = project.ClientDeadline?.ConvertFromUnixTime();
        OrderedOn = project.OrderedOn?.ConvertFromUnixTime();
        SourceLanguageId = project.Languages.SourceLanguageId;
        TargetLanguageIds = project.Languages.TargetLanguageIds;
        LanguageCombinations = project.Languages.LanguageCombinations;
        ProjectConfirmationStatus = project.Documents.ProjectConfirmationStatus;
    }

    [Display("Project ID")]
    public string Id { get; set; }
    
    [Display("Is classic project")]
    public bool IsClassicProject { get; set; }
    
    [Display("Project ID number")]
    public string ProjectIdNumber { get; set; }
    
    [Display("Quote ID number")]
    public string QuoteIdNumber { get; set; }
    
    public string Name { get; set; }
    
    [Display("Client ID")]
    public string ClientId { get; set; }
    
    [Display("Service ID")]
    public string ServiceId { get; set; }

    [Display("Project manager ID")]
    public string ProjectManagerId { get; set; }
    
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }
    
    public string Status { get; set; }
    
    [Display("Budget code")]
    public string BudgetCode { get; set; }
    
    public string Origin { get; set; }
    
    [Display("Client reference number")]
    public string ClientReferenceNumber { get; set; }
    
    [Display("Client notes")]
    public string ClientNotes { get; set; }
    
    [Display("Internal notes")]
    public string InternalNotes { get; set; }
    
    [Display("Instructions for all jobs")]
    public string InstructionsForAllJobs { get; set; }
    
    [Display("Client deadline")]
    public DateTime? ClientDeadline { get; set; }
    
    [Display("Ordered on")]
    public DateTime? OrderedOn { get; set; }
    
    [Display("Source language")]
    public string SourceLanguageId { get; set; }
    
    [Display("Target languages")]
    public IEnumerable<string> TargetLanguageIds { get; set; }
    
    [Display("Language combinations")]
    public IEnumerable<SmartJobLanguageCombination> LanguageCombinations { get; set; }
    
    [Display("Project confirmation status")]
    public string ProjectConfirmationStatus { get; set; }
    
    [Display("Project contacts")]
    public SmartProjectContacts ProjectContacts { get; set; }
    
    [Display("Finance information")]
    public FinanceInformation FinanceInformation { get; set; }
    
    [Display("Process ID")]
    public string ProcessId { get; set; }
    
    [Display("Is project created in CAT tool or creation is queued")]
    public bool ProjectCreatedInCatToolOrCreationIsQueued { get; set; }
}