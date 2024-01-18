using Apps.XTRF.Shared.Webhooks.Handlers.Base;
using Apps.XTRF.Shared.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class QuoteStatusChangedHandler : WebhookHandlerWithFilter
{
    const string SubscriptionEvent = "quote_status_changed";

    public QuoteStatusChangedHandler([WebhookParameter(true)] FilterStringInput filterString) 
        : base(SubscriptionEvent, filterString)
    {
    }
}