using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Entities;

public class SmartQuote
{
    public string Id { get; set; }
    public bool IsClassicQuote { get; set; }
    public string QuoteIdNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string BudgetCode { get; set; }
    public string ClientId { get; set; }
    public string ServiceId { get; set; }
    public string Origin { get; set; }
    public string ClientReferenceNumber { get; set; }
    public string ClientNotes { get; set; }
    public string InternalNotes { get; set; }
    public string InstructionsForAllJobs { get; set; }
    public long? ClientDeadline { get; set; }
    public long? OrderedOn { get; set; }
    public long? QuoteExpiry { get; set; }
    public long? ExpectedDeliveryDate { get; set; }
    public int BusinessDays { get; set; }
    public SmartProjectLanguages Languages { get; set; }
    public SmartProjectDocuments Documents { get; set; }
    public SmartProjectPeople People { get; set; }
    public SmartQuoteVolume Volume { get; set; }
}

public class SmartQuoteVolume
{
    public double? Value { get; set; }
    
    [Display("Unit ID")]
    public string? UnitId { get; set; }
} 