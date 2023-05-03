using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class QuoteCreatedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "quote_created";
        public QuoteCreatedHandler() : base(SubscriptionEvent) { }
    }
}
