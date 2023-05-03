    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class ProjectStatusChangedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "project_status_changed";
        public ProjectStatusChangedHandler() : base(SubscriptionEvent) { }
    }
}
