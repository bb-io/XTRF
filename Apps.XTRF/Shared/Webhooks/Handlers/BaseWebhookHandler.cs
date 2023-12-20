using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Webhooks.Models.Request;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.XTRF.Shared.Webhooks.Handlers;

public class BaseWebhookHandler : IWebhookEventHandler
{
    private readonly string _event;

    public BaseWebhookHandler(string subEvent)
    {
        _event = subEvent;
    }

    public Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var client = new XtrfClient(creds);
        var request = new XtrfRequest("/subscription", Method.Post, creds);
        request.AddJsonBody(new SubscribeRequest
        {
            Url = values["payloadUrl"],
            Event = _event
        });

        return client.ExecuteWithErrorHandling(request);
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var client = new XtrfClient(creds);

        var result = await GetAllWebhooks(client, creds);
        var currentSubscription = result.FirstOrDefault(x => x.Url == values["payloadUrl"]);

        if (currentSubscription is null)
            return;

        var endpoint = $"/subscription/{currentSubscription?.SubscriptionId}";
        var request = new XtrfRequest(endpoint, Method.Delete, creds);

        await client.ExecuteWithErrorHandling(request);
    }

    private Task<List<Subscription>> GetAllWebhooks(XtrfClient client,
        IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new XtrfRequest("/subscription", Method.Get, creds);
        return client.ExecuteWithErrorHandling<List<Subscription>>(request);
    }
}