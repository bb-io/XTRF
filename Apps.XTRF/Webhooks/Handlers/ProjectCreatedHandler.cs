namespace Apps.XTRF.Webhooks.Handlers;

public class ProjectCreatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "project_created";

    public ProjectCreatedHandler() : base(SubscriptionEvent)
    {
    }
}