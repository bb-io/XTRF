    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class JobStatusChangedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "job_status_changed";
        public JobStatusChangedHandler() : base(SubscriptionEvent) { }
    }
}
