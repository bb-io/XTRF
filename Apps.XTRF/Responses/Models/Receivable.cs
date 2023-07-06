namespace Apps.XTRF.Responses.Models
{
    public class Receivable
    {
        public int Id { get; set; }
        public int JobTypeId { get; set; }
        public int CurrencyId { get; set; }
        public double Total { get; set; }
        public string? InvoiceId { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
    }
}
