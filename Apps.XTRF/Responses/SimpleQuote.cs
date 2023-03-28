using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class SimpleQuote
    {
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public int OpportunityOfferId { get; set; }
    }
}
