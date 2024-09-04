using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Invoices;

public class ProviderInvoiceResponse
{
    [Display("Invoice ID")]
    public string Id { get; set; } = string.Empty;
        
    [Display("Total gross amount")]
    public double TotalGross { get; set; } 
        
    [Display("Total net amount")]
    public double TotalNetto { get; set; }
        
    [Display("Currency ID")]
    public string CurrencyId { get; set; } = string.Empty;
        
    public string Status { get; set; } = string.Empty;

    [Display("Final number")]
    public string? FinalNumber { get; set; }

    [Display("Draft number")]
    public string? DraftNumber { get; set; }
        
    [Display("Internal number")]
    public string InternalNumber { get; set; } = string.Empty;

    [Display("Provider ID")]
    public string ProviderId { get; set; } = string.Empty;

    [Display("Total gross in words")]
    public string TotalGrossInWords { get; set; } = string.Empty;
        
    [Display("Jobs net value")]
    public double JobsNetValue { get; set; }

    [Display("Payment status")]
    public string PaymentStatus { get; set; } = string.Empty;

    [Display("Invoice dates")]
    public InvoiceDatesResponse Dates { get; set; } = new();

    [Display("Notes from provider")]
    public string? NotesFromProvider { get; set; }

    public List<PaymentResponse> Payments { get; set; } = new();
}

public class InvoiceDatesResponse
{
    [Display("Draft date")]
    public BaseDateResponse? DraftDate { get; set; } 
        
    [Display("Final date")]
    public BaseDateResponse? FinalDate { get; set; }
        
    [Display("Payment due date")]
    public BaseDateResponse? PaymentDueDate { get; set; }
        
    [Display("Invoice uploaded date")]
    public BaseDateResponse? InvoiceUploadedDate { get; set; }
}