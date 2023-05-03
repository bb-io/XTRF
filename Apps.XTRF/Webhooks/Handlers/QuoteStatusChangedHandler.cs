using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class QuoteStatusChangedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "quote_status_changed";
        public QuoteStatusChangedHandler() : base(SubscriptionEvent) { }
    }
}
