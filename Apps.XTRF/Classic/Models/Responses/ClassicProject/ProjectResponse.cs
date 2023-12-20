using Apps.XTRF.Classic.Models.Responses.ClassicTask;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Responses.ClassicProject;

public class ProjectResponse
{
    public ProjectResponse(Entities.ClassicProject project)
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
        StartDate = project.Dates.StartDate?.Time?.ConvertFromUnixTime();
        Deadline = project.Dates.Deadline?.Time?.ConvertFromUnixTime();
        ActualStartDate = project.Dates.ActualStartDate?.Time?.ConvertFromUnixTime();
        ActualDeliveryDate = project.Dates.ActualDeliveryDate?.Time?.ConvertFromUnixTime();
        Instructions = project.Instructions;
        Finance = project.Finance;
        Contacts = project.Contacts;
        CategoriesIds = project.CategoriesIds;
        CustomFields = project.CustomFields;
        Tasks = project.Tasks?.Select(task => new TaskResponse(task));
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
    
    public ProjectContacts Contacts { get; set; }
    
    [Display("Categories IDs")]
    public IEnumerable<string> CategoriesIds { get; set; }
    
    [Display("Custom fields")]
    public IEnumerable<CustomField> CustomFields { get; set; }
    
    public IEnumerable<TaskResponse> Tasks { get; set; }
}