using Apps.XTRF.Shared.Webhooks.Handlers.Base;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class CustomerCreatedHandler : SimpleWebhookHandler
{
    const string SubscriptionEvent = "customer_created";

    public CustomerCreatedHandler() : base(SubscriptionEvent)
    {
    }
}