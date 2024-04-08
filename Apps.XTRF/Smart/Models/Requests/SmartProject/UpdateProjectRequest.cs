using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.SmartProject;

public class UpdateProjectRequest
{
    [StaticDataSource(typeof(ProjectStatusDataHandler))]
    public string? Status { get; set; }
    
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? SourceLanguageId { get; set; }
    
    [Display("Target languages")]
    [DataSource(typeof(LanguageDataHandler))]
    public IEnumerable<string>? TargetLanguageIds { get; set; }
    
    [Display("Specialization")]
    [DataSource(typeof(SpecializationDataHandler))]
    public string? SpecializationId { get; set; }
    
    [Display("Primary contact person")]
    [DataSource(typeof(ProjectContactDataHandler))]
    public string? PrimaryId { get; set; }

    [Display("Additional contact persons")]
    [DataSource(typeof(ProjectContactDataHandler))]
    public IEnumerable<string>? AdditionalIds { get; set; }
    
    [Display("Client deadline")]
    public DateTime? ClientDeadline { get; set; }
    
    [Display("Order date")]
    public DateTime? OrderDate { get; set; }
    
    [Display("Client notes")]
    public string? ClientNotes { get; set; }
    
    [Display("Internal notes")]
    public string? InternalNotes { get; set; }
    
    [Display("Vendor instructions")]
    public string? VendorInstructions { get; set; }
    
    [Display("Client reference number")]
    public string? ClientReferenceNumber { get; set; }
    
    public int? Volume { get; set; }
}