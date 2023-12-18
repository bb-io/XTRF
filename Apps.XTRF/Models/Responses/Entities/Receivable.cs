using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Entities;

public class Receivable
{
    [Display("Receivable ID")]
    public string Id { get; set; }
    
    [Display("Job type ID")]
    public string JobTypeId { get; set; }
    
    [Display("Currency ID")]
    public string CurrencyId { get; set; }
    
    public double Total { get; set; }
    
    [Display("Invoice ID")]
    public string? InvoiceId { get; set; }
    
    public string Type { get; set; }
    
    public string? Description { get; set; }
    
    [Display("Language combination")]
    public LanguageCombination LanguageCombination { get; set; }
}