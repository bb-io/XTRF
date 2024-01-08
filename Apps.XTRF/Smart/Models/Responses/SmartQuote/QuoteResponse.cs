using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.SmartQuote;

public class QuoteResponse
{
    public QuoteResponse(Entities.SmartQuote quote)
    {
        Id = quote.Id;
        IsClassicQuote = quote.IsClassicQuote;
        QuoteIdNumber = quote.QuoteIdNumber;
        Name = quote.Name;
        ClientId = quote.ClientId;
        ServiceId = quote.ServiceId;
        ProjectManagerId = quote.People.ProjectManagerId;
        SpecializationId = quote.Languages.SpecializationId;
        Status = quote.Status;
        BudgetCode = quote.BudgetCode;
        Origin = quote.Origin;
        ClientReferenceNumber = quote.ClientReferenceNumber;
        ClientNotes = quote.ClientNotes;
        InternalNotes = quote.InternalNotes;
        InstructionsForAllJobs = quote.InstructionsForAllJobs;
        ClientDeadline = quote.ClientDeadline?.ConvertFromUnixTime();
        OrderedOn = quote.OrderedOn?.ConvertFromUnixTime();
        QuoteExpiry = quote.QuoteExpiry?.ConvertFromUnixTime();
        ExpectedDeliveryDate = quote.ExpectedDeliveryDate?.ConvertFromUnixTime();
        BusinessDays = quote.BusinessDays;
        SourceLanguageId = quote.Languages.SourceLanguageId;
        TargetLanguageIds = quote.Languages.TargetLanguageIds;
        LanguageCombinations = quote.Languages.LanguageCombinations;
        ProjectConfirmationStatus = quote.Documents.ProjectConfirmationStatus;
    }
    
    [Display("ID")] 
    public string Id { get; set; }
    
    [Display("Is classic quote")]
    public bool IsClassicQuote { get; set; }
    
    [Display("Quote ID number")] 
    public string QuoteIdNumber { get; set; }
    
    public string Name { get; set; }
    
    public string Status { get; set; }
    
    [Display("Budget code")] 
    public string BudgetCode { get; set; }

    [Display("Client ID")]
    public string ClientId { get; set; }

    [Display("Service ID")]
    public string ServiceId { get; set; }
    
    [Display("Project manager ID")]
    public string ProjectManagerId { get; set; }
    
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }

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
    
    [Display("Quote expiration date")]
    public DateTime? QuoteExpiry { get; set; }
    
    [Display("Expected delivery date")]
    public DateTime? ExpectedDeliveryDate { get; set; }

    [Display("Business days")]
    public int BusinessDays { get; set; }
    
    [Display("Source language")]
    public string SourceLanguageId { get; set; }
    
    [Display("Target languages")]
    public IEnumerable<string> TargetLanguageIds { get; set; }
    
    [Display("Language combinations")]
    public IEnumerable<SmartJobLanguageCombination> LanguageCombinations { get; set; }
    
    [Display("Project confirmation status")]
    public string ProjectConfirmationStatus { get; set; }
    
    [Display("Finance information")]
    public FinanceInformation FinanceInformation { get; set; }
}