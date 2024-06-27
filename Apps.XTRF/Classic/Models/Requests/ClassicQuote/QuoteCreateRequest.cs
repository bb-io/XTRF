using Apps.XTRF.Classic.DataSourceHandlers;
using Apps.XTRF.Classic.DataSourceHandlers.EnumHandlers;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XTRF.Classic.Models.Requests.ClassicQuote;

public class QuoteCreateRequest : PersonIdentifier
{
    [Display("Quote name")]
    public string QuoteName { get; set; }
    
    [Display("Customer project number")]
    public string? CustomerProjectNumber { get; set; }
    
    public IEnumerable<FileReference>? Files { get; set; }

    [Display("Service ID"), DataSource(typeof(ClassicServiceDataSourceHandler))]
    public string ServiceId { get; set; }
    
    [Display("Source language ID"), DataSource(typeof(ClassicLanguageDataSource))]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language IDs"), DataSource(typeof(ClassicLanguageDataSource))]
    public IEnumerable<string>? TargetLanguageIds { get; set; }
    
    [Display("Specialization ID"), DataSource(typeof(ClassicSpecializationDataSource))]
    public string SpecializationId { get; set; }

    [Display("Delivery date", Description = "By default, the delivery date is set to the current date + 7 days.")]
    public DateTime? DeliveryDate { get; set; }

    [Display("Note")]
    public string? Note { get; set; }

    [Display("Price profile ID"), DataSource(typeof(ClassicPriceProfileDataSource))]
    public string PriceProfileId { get; set; }

    [Display("Send back to ID"), DataSource(typeof(ClassicPersonDataSource))]
    public string? SendBackToId { get; set; }

    [Display("Additional person IDs"), DataSource(typeof(ClassicPersonDataSource))]
    public IEnumerable<string>? AdditionalPersonIds { get; set; }
    
    [Display("Additional email addresses")]
    public IEnumerable<FileReference>? ReferenceFiles { get; set; }

    [Display("Office ID"), DataSource(typeof(ClassicOfficeDataSource))]
    public string? OfficeId { get; set; }
    
    [Display("Budget code")]
    public string? BudgetCode { get; set; }

    [Display("Cat tool type", Description = "By default, the value is set to 'Trados'."), StaticDataSource(typeof(ClassicCatToolTypeDataSource))]
    public string? CatToolType { get; set; }
}