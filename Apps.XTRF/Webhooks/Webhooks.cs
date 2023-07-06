using Apps.XTRF.Webhooks.Handlers;
using Apps.XTRF.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;

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
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                Result = data
            };
        }

        [Webhook("On project status changed", typeof(ProjectStatusChangedHandler), Description = "Triggered when the status of an XTRF project is changed")]
        public async Task<WebhookResponse<ProjectStatusChangedPayload>> ProjectStatusChangedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<ProjectStatusChangedPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<ProjectStatusChangedPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                Result = data
            };
        }

        [Webhook("On quote created", typeof(QuoteCreatedHandler), Description = "Triggered when a new XTRF quote is created")]
        public async Task<WebhookResponse<QuoteCreatedPayload>> QuoteCreatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<QuoteCreatedPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<QuoteCreatedPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                Result = data
            };
        }

        [Webhook("On quote status changed", typeof(QuoteStatusChangedHandler), Description = "Triggered when the status of an XTRF quote is changed")]
        public async Task<WebhookResponse<QuoteStatusChangedPayload>> QuoteStatusChangedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<QuoteStatusChangedPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<QuoteStatusChangedPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                Result = data
            };
        }

        [Webhook("On job status changed", typeof(JobStatusChangedHandler), Description = "Triggered when the status of an XTRF job is changed")]
        public async Task<WebhookResponse<JobStatusChangedPayload>> JobStatusChangedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<JobStatusChangedPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<JobStatusChangedPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                Result = data
            };
        }

        [Webhook("On customer created", typeof(CustomerCreatedHandler), Description = "Triggered when a new XTRF customer is created")]
        public async Task<WebhookResponse<CustomerPayload>> CustomerCreatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<CustomerPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<CustomerPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                Result = data
            };
        }

        [Webhook("On customer updated", typeof(CustomerUpdatedHandler), Description = "Triggered when a new XTRF customer is updated")]
        public async Task<WebhookResponse<CustomerPayload>> CustomerUpdatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<CustomerPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<CustomerPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                Result = data
            };
        }

    }

}
