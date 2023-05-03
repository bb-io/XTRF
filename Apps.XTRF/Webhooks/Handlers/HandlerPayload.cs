using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class HandlerPayload
    {
        public string Url { get; set; }
        public string Event { get; set; }
        //public string Filter { get; set; }
        //public string Embed { get; set; }
    }
}
