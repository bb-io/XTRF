namespace Apps.XTRF.Shared.Models.Entities;

public class CreateInvoiceEntity
{ 
    public int[] InvoicesIds { get; set; } = Array.Empty<int>();
    
    public string InvoiceUrl { get; set; } = string.Empty;
}