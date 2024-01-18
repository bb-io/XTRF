using Apps.XTRF.Shared.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.XTRF.Shared.Webhooks.Handlers.Base;

public class WebhookHandlerWithFilter : BaseWebhookHandler, IWebhookEventHandler<FilterStringInput>
{
    private readonly FilterStringInput _filterString;

    protected WebhookHandlerWithFilter(string subscriptionEvent, [WebhookParameter(true)] FilterStringInput filterString)
        : base(subscriptionEvent)
    {
        _filterString = filterString;
    }

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
        => await SubscribeAsync(creds, values, _filterString.FilterString);
}