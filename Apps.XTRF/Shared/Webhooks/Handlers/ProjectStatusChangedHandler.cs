using Apps.XTRF.Shared.Webhooks.Handlers.Base;
using Apps.XTRF.Shared.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class ProjectStatusChangedHandler : WebhookHandlerWithFilter
{
    const string SubscriptionEvent = "project_status_changed";

    public ProjectStatusChangedHandler([WebhookParameter(true)] FilterStringInput filterString) 
        : base(SubscriptionEvent, filterString)
    {
    }
}