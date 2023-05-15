using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
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
