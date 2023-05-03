using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class CustomerUpdatedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "customer_updated";
        public CustomerUpdatedHandler() : base(SubscriptionEvent) { }
    }
}
