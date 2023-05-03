using Apps.XTRF.Webhooks.Handlers;
using Apps.XTRF.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks
{
    [WebhookList]
    public class Webhooks
    {
        [Webhook("On project created", typeof(ProjectCreatedHandler), Description = "Triggered when a new XTRF project is created")]
        public async Task<WebhookResponse<ProjectCreatedPayload>> ProjectCreatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<ProjectCreatedPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<ProjectCreatedPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }
    }
}
