using Apps.XTRF.Shared.Webhooks.Handlers;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Webhooks.Models.Payloads;
using Apps.XTRF.Shared.Webhooks.Models.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Shared.Webhooks;

[WebhookList]
public class WebhookList(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
{
    #region Webhooks

    [Webhook("On project created", typeof(ProjectCreatedHandler),
        Description = "Triggered when a new XTRF project is created")]
    public Task<WebhookResponse<ProjectCreatedPayload>> ProjectCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<ProjectCreatedPayload>(webhookRequest);

    [Webhook("On project status changed", typeof(ProjectStatusChangedHandler),
        Description = "Triggered when the status of an XTRF project is changed")]
    public async Task<WebhookResponse<ProjectStatusChangedPayload>> ProjectStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] [Display("Project status")] [StaticDataSource(typeof(ProjectStatusDataHandler))]
        string? status,
        [WebhookParameter] ProjectOptionalRequest projectOptionalRequest)
    {
        var result = await HandleWebhook<ProjectStatusChangedPayload>(webhookRequest, 
            status != null ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) : null);
        
        if (result.Result != null && projectOptionalRequest.ProjectId != null && !result.Result.InternalId.Equals(projectOptionalRequest.ProjectId))
        {
            return GetPreflightResponse<ProjectStatusChangedPayload>();
        }

        return result;
    }

    [Webhook("On quote created", typeof(QuoteCreatedHandler),
        Description = "Triggered when a new XTRF quote is created")]
    public Task<WebhookResponse<QuoteCreatedPayload>> QuoteCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<QuoteCreatedPayload>(webhookRequest);

    [Webhook("On quote status changed", typeof(QuoteStatusChangedHandler),
        Description = "Triggered when the status of an XTRF quote is changed")]
    public async Task<WebhookResponse<QuoteStatusChangedPayload>> QuoteStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] [Display("Quote status")] [StaticDataSource(typeof(QuoteStatusDataHandler))]
        string? status,
        [WebhookParameter] QuoteOptionalRequest quoteOptionalRequest)
    {
        var result = await HandleWebhook<QuoteStatusChangedPayload>(webhookRequest,
            status != null ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) : null);
        
        if (result.Result != null && quoteOptionalRequest.QuoteId != null && !result.Result.InternalId.Equals(quoteOptionalRequest.QuoteId)) 
        {
            return GetPreflightResponse<QuoteStatusChangedPayload>();
        }
        
        return result;
    }

    [Webhook("On job status changed", typeof(JobStatusChangedHandler),
        Description = "Triggered when the status of an XTRF job is changed")]
    public async Task<WebhookResponse<JobStatusChangedPayload>> JobStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] [Display("Job status")] [StaticDataSource(typeof(JobStatusDataHandler))]
        string? status,
        [WebhookParameter] ProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] TaskOptionalRequest taskOptionalRequest,
        [WebhookParameter] JobOptionalRequest jobOptionalRequest)
    {
        var result = await HandleWebhook<JobStatusChangedPayload>(webhookRequest,
            status != null ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) : null);

        if (result.Result != null)
        {
            if (projectOptionalRequest.ProjectId != null &&
                !result.Result.ProjectInternalId.Equals(projectOptionalRequest.ProjectId))
            {
                return GetPreflightResponse<JobStatusChangedPayload>();
            }

            if (taskOptionalRequest.TaskId != null && !result.Result.TaskId.Equals(taskOptionalRequest.TaskId))
            {
                return GetPreflightResponse<JobStatusChangedPayload>();
            }

            if (jobOptionalRequest.JobId != null && !result.Result.JobInternalId.Equals(jobOptionalRequest.JobId))
            {
                return GetPreflightResponse<JobStatusChangedPayload>();
            }

            if (jobOptionalRequest.JobTypeName != null &&
               !result.Result.JobType.Equals(jobOptionalRequest.JobTypeName, StringComparison.OrdinalIgnoreCase))
            {
                return GetPreflightResponse<JobStatusChangedPayload>();
            }
        }
        
        return result;
    }

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
    
    private WebhookResponse<T> GetPreflightResponse<T>()
        where T : class
        => new()
        {
            HttpResponseMessage = new HttpResponseMessage(statusCode: HttpStatusCode.OK),
            ReceivedWebhookRequestType = WebhookRequestType.Preflight
        };

    #endregion
}