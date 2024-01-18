using Apps.XTRF.Shared.Webhooks.Handlers.Base;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class JobStatusChangedHandler : SimpleWebhookHandler
{
    const string SubscriptionEvent = "job_status_changed";

    public JobStatusChangedHandler() : base(SubscriptionEvent)
    {
    }
}