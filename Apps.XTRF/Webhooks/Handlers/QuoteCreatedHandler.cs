namespace Apps.XTRF.Webhooks.Handlers
{
    public class QuoteCreatedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "quote_created";
        public QuoteCreatedHandler() : base(SubscriptionEvent) { }
    }
}
