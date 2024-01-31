using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.XTRF.Shared.Webhooks.Handlers.Base;

public class SimpleWebhookHandler : BaseWebhookHandler, IWebhookEventHandler
{
    protected SimpleWebhookHandler(string subscriptionEvent) : base(subscriptionEvent)
    {
    }

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, 
        Dictionary<string, string> values)
        => await SubscribeAsync(creds, values, null);
}