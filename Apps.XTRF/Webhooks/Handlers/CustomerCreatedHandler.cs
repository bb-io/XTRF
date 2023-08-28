namespace Apps.XTRF.Webhooks.Handlers;

public class CustomerCreatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "customer_created";
    public CustomerCreatedHandler() : base(SubscriptionEvent) { }
}