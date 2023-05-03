using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class CustomerCreatedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "customer_created";
        public CustomerCreatedHandler() : base(SubscriptionEvent) { }
    }
}
