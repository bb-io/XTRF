using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Smart.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.SmartQuote;

public class UpdateQuoteRequest
{
    [DataSource(typeof(SmartQuoteStatusDataSourceHandler))]
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
    [DataSource(typeof(SmartQuoteContactDataHandler))]
    public string? PrimaryId { get; set; }

    [Display("Additional contact persons")]
    [DataSource(typeof(SmartQuoteContactDataHandler))]
    public IEnumerable<string>? AdditionalIds { get; set; }
    
    [Display("Business days")]
    public int? BusinessDays { get; set; }
    
    [Display("Client notes")]
    public string? ClientNotes { get; set; }
    
    [Display("Internal notes")]
    public string? InternalNotes { get; set; }
    
    [Display("Vendor instructions")]
    public string? VendorInstructions { get; set; }
    
    [Display("Client reference number")]
    public string? ClientReferenceNumber { get; set; }
    
    [Display("Expected delivery date")]
    public DateTime? ExpectedDeliveryDate { get; set; }
    
    [Display("Quote expiration date")]
    public DateTime? QuoteExpiry { get; set; }
    
    public int? Volume { get; set; }
}