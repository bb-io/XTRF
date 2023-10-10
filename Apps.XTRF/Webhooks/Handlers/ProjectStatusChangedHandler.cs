namespace Apps.XTRF.Webhooks.Handlers;

public class ProjectStatusChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "project_status_changed";

    public ProjectStatusChangedHandler() : base(SubscriptionEvent)
    {
    }
}