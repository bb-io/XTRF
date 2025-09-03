using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Entities;

public class FinanceInformation
{
    [Display("Currency ID")]
    public string? CurrencyId { get; set; }
        
    [Display("Total agreed")] 
    public double? TotalAgreed { get; set; }
    
    [Display("Total cost")] 
    public double? TotalCost { get; set; }
    
    public double? Profit { get; set; }
    
    public double? Margin { get; set; }
    
    public IEnumerable<Payable>? Payables { get; set; } = Enumerable.Empty<Payable>();
    
    public IEnumerable<Receivable>? Receivables { get; set; } = Enumerable.Empty<Receivable>();
    
    public double? ROI { get; set; }
}