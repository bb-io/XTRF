using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.XTRF.Webhooks.Handlers
{
    public class BaseWebhookHandler : IWebhookEventHandler
    {
        private string _event;
        public BaseWebhookHandler(string subEvent)
        {
            _event = subEvent;
        }

        public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            var client = new XtrfClient(authenticationCredentialsProvider);
            var request = new XtrfRequest($"/subscription", Method.Post, authenticationCredentialsProvider);
            request.AddJsonBody(new HandlerPayload
            {
                Url = values["payloadUrl"],
                Event = _event
            });
            client.Execute(request);
        }

        public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            var client = new XtrfClient(authenticationCredentialsProvider);
            var request = new XtrfRequest($"/subscription", Method.Get, authenticationCredentialsProvider);
            var result = client.Get<List<Subscription>>(request);
            var currentSubscription = result.FirstOrDefault(x => x.Url == values["payloadUrl"]);

            var deleteRequest = new XtrfRequest($"/subscription/{currentSubscription?.SubscriptionId}", Method.Delete, authenticationCredentialsProvider);
            client.Execute(request);
        }
    }
}
