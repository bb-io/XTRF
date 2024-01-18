using Apps.XTRF.Shared.Webhooks.Handlers.Base;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class ProjectCreatedHandler : SimpleWebhookHandler
{
    const string SubscriptionEvent = "project_created";

    public ProjectCreatedHandler() : base(SubscriptionEvent)
    {
    }
}