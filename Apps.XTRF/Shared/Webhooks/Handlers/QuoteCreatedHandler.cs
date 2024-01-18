using Apps.XTRF.Shared.Webhooks.Handlers.Base;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class QuoteCreatedHandler : SimpleWebhookHandler
{
    const string SubscriptionEvent = "quote_created";

    public QuoteCreatedHandler() : base(SubscriptionEvent)
    {
    }
}