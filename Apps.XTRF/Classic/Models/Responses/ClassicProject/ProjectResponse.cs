using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Responses.ClassicTask;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Responses.ClassicProject;

public class ProjectResponse
{
    public ProjectResponse(Entities.ClassicProject project, XtrfTimeZoneInfo timeZoneInfo)
    {
        Id = project.Id;
        IsClassicProject = project.IsClassicProject;
        IdNumber = project.IdNumber;
        Name = project.Name;
        CustomerId = project.CustomerId;
        ContactPersonId = project.ContactPersonId;
        ProjectManagerId = project.ProjectManagerId;
        SpecializationId = project.SpecializationId;
        Status = project.Status;
        StartDate = project.Dates.StartDate?.Time?.ConvertFromUnixTime(timeZoneInfo);
        Deadline = project.Dates.Deadline?.Time?.ConvertFromUnixTime(timeZoneInfo);
        ActualStartDate = project.Dates.ActualStartDate?.Time?.ConvertFromUnixTime(timeZoneInfo);
        ActualDeliveryDate = project.Dates.ActualDeliveryDate?.Time?.ConvertFromUnixTime(timeZoneInfo);
        Instructions = project.Instructions;
        Finance = project.Finance;
        Contacts = project.Contacts;
        CategoriesIds = project.CategoriesIds;
        Tasks = project.Tasks?.Select(task => new TaskResponse(task, timeZoneInfo)) ?? new List<TaskResponse>();
    }
    
    [Display("Project ID")]
    public string Id { get; set; }
    
    [Display("Is classic project")]
    public bool IsClassicProject { get; set; }
    
    [Display("ID number")]
    public string IdNumber { get; set; }
    
    public string Name { get; set; }
    
    [Display("Customer ID")]
    public string CustomerId { get; set; }
    
    [Display("Contact person ID")]
    public string ContactPersonId { get; set; }
    
    [Display("Project manager ID")]
    public string ProjectManagerId { get; set; }
    
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }
    
    public string Status { get; set; }
    
    [Display("Start date")]
    public DateTime? StartDate { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    [Display("Actual start date")]
    public DateTime? ActualStartDate { get; set; }
    
    [Display("Actual delivery date")]
    public DateTime? ActualDeliveryDate { get; set; }
    
    public Instructions Instructions { get; set; }
    
    public FinanceInformation Finance { get; set; }
    
    public ClassicProjectContacts Contacts { get; set; }
    
    [Display("Categories IDs")]
    public IEnumerable<string> CategoriesIds { get; set; }

    public IEnumerable<TaskResponse> Tasks { get; set; }
}