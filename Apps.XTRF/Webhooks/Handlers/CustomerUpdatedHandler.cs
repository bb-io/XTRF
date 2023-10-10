namespace Apps.XTRF.Webhooks.Handlers;

public class CustomerUpdatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "customer_updated";

    public CustomerUpdatedHandler() : base(SubscriptionEvent)
    {
    }
}