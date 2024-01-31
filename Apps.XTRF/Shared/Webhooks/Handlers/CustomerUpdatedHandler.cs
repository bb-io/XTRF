using Apps.XTRF.Shared.Webhooks.Handlers.Base;
using Apps.XTRF.Shared.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class CustomerUpdatedHandler : WebhookHandlerWithFilter
{
    const string SubscriptionEvent = "customer_updated";

    public CustomerUpdatedHandler([WebhookParameter(true)] FilterStringInput filterString) 
        : base(SubscriptionEvent, filterString)
    {
    }
}