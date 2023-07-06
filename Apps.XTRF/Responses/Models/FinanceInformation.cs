using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models
{
    public class FinanceInformation
    {
        [Display("Currency id")] public int CurrencyId { get; set; }
        [Display("Total agreed")] public double TotalAgreed { get; set; }
        [Display("Total cost")] public double TotalCost { get; set; }
        public double Profit { get; set; }
        public double Margin { get; set; }
        public IEnumerable<Payable> Payables { get; set; }
        public IEnumerable<Receivable> Receivables { get; set; }
        public double ROI { get; set; }
    }
}
