namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class JobStatusChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "job_status_changed";

    public JobStatusChangedHandler() : base(SubscriptionEvent)
    {
    }
}