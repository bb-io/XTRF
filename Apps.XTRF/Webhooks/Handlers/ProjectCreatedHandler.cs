using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class ProjectCreatedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "project_created";
        public ProjectCreatedHandler() : base(SubscriptionEvent) { }
    }
}
