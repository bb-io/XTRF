using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class Subscription
    {
        public string Url { get; set; }
        public string Event { get; set; }
        public string SubscriptionId { get; set; }
        //public string Filter { get; set; }
        //public string Embed { get; set; }
    }
}
