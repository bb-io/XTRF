using Apps.XTRF.Webhooks.Handlers;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;
using Apps.XTRF.Webhooks.DataSourceHandlers;
using Apps.XTRF.Webhooks.Models.Payloads;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Webhooks;

[WebhookList]
public class WebhookList
{
    #region Webhooks

    [Webhook("On project created", typeof(ProjectCreatedHandler),
        Description = "Triggered when a new XTRF project is created")]
    public Task<WebhookResponse<ProjectCreatedPayload>> ProjectCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<ProjectCreatedPayload>(webhookRequest);

    [Webhook("On project status changed", typeof(ProjectStatusChangedHandler),
        Description = "Triggered when the status of an XTRF project is changed")]
    public Task<WebhookResponse<ProjectStatusChangedPayload>> ProjectStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] [Display("Project status")] [DataSource(typeof(ProjectStatusDataSourceHandler))]
        string? status)
        => HandleWebhook<ProjectStatusChangedPayload>(webhookRequest, 
            status != null ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) : null);

    [Webhook("On quote created", typeof(QuoteCreatedHandler),
        Description = "Triggered when a new XTRF quote is created")]
    public Task<WebhookResponse<QuoteCreatedPayload>> QuoteCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<QuoteCreatedPayload>(webhookRequest);

    [Webhook("On quote status changed", typeof(QuoteStatusChangedHandler),
        Description = "Triggered when the status of an XTRF quote is changed")]
    public Task<WebhookResponse<QuoteStatusChangedPayload>> QuoteStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] [Display("Quote status")] [DataSource(typeof(QuoteStatusDataSourceHandler))]
        string? status)
        => HandleWebhook<QuoteStatusChangedPayload>(webhookRequest,
            status != null ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) : null);

    [Webhook("On job status changed", typeof(JobStatusChangedHandler),
        Description = "Triggered when the status of an XTRF job is changed")]
    public Task<WebhookResponse<JobStatusChangedPayload>> JobStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] [Display("Job status")] [DataSource(typeof(JobStatusDataSourceHandler))]
        string? status)
        => HandleWebhook<JobStatusChangedPayload>(webhookRequest,
            status != null ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) : null);

    [Webhook("On customer created", typeof(CustomerCreatedHandler),
        Description = "Triggered when a new XTRF customer is created")]
    public Task<WebhookResponse<CustomerPayload>> CustomerCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<CustomerPayload>(webhookRequest);

    [Webhook("On customer updated", typeof(CustomerUpdatedHandler),
        Description = "Triggered when a new XTRF customer is updated")]
    public Task<WebhookResponse<CustomerPayload>> CustomerUpdatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<CustomerPayload>(webhookRequest);

    #endregion

    #region Utils
    
    private Task<WebhookResponse<T>> HandleWebhook<T>(WebhookRequest webhookRequest, Func<T, bool>? predicate = null) 
        where T : class
    {
        var payload = webhookRequest.Body.ToString();
        ArgumentException.ThrowIfNullOrEmpty(payload);
        
        var data = JsonConvert.DeserializeObject<T>(payload);

        if (data is null)
            throw new InvalidCastException(nameof(webhookRequest.Body));

        if (predicate != null && !predicate(data))
            return Task.FromResult<WebhookResponse<T>>(new()
            {
                HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });

        return Task.FromResult<WebhookResponse<T>>(new()
        {
            HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
            Result = data
        });
    }

    #endregion
}