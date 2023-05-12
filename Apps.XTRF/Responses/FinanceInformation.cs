using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class FinanceInformation
    {
        public int CurrencyId { get; set; }
        public double TotalAgreed { get; set; }
        public double TotalCost { get; set; }
        public double Profit { get; set; }
        public double Margin { get; set; }
        public IEnumerable<Payable> Payables { get; set; }
        public IEnumerable<Receivable> Receivables { get; set; }
        public double ROI { get; set; }
    }
}
