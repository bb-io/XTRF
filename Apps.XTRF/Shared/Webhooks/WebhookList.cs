using Apps.XTRF.Shared.Webhooks.Handlers;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Webhooks.Models.Inputs;
using Apps.XTRF.Shared.Webhooks.Models.Payloads;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.XTRF.Shared.Webhooks;

[WebhookList]
public class WebhookList : XtrfInvocable
{
    public WebhookList(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    #region Webhooks

    [Webhook("On project created", typeof(ProjectCreatedHandler),
        Description = "Triggered when a new XTRF project is created")]
    public Task<WebhookResponse<ProjectCreatedPayload>> ProjectCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<ProjectCreatedPayload>(webhookRequest);

    [Webhook("On project status changed", typeof(ProjectStatusChangedHandler),
        Description = "Triggered when the status of an XTRF project is changed")]
    public Task<WebhookResponse<ProjectStatusChangedPayload>> ProjectStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] CustomFieldFilterInput customFieldFilterInput,
        [WebhookParameter] [Display("Project status")] [DataSource(typeof(ProjectStatusDataHandler))]
        string? status)
        => HandleWebhook<ProjectStatusChangedPayload>(webhookRequest,
            status != null
                ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) &&
                             CheckIfCustomFieldFiltersApply(customFieldFilterInput, EntityType.Project, payload.Id)
                : payload => CheckIfCustomFieldFiltersApply(customFieldFilterInput, EntityType.Project, payload.Id));

    [Webhook("On quote created", typeof(QuoteCreatedHandler),
        Description = "Triggered when a new XTRF quote is created")]
    public Task<WebhookResponse<QuoteCreatedPayload>> QuoteCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<QuoteCreatedPayload>(webhookRequest);

    [Webhook("On quote status changed", typeof(QuoteStatusChangedHandler),
        Description = "Triggered when the status of an XTRF quote is changed")]
    public Task<WebhookResponse<QuoteStatusChangedPayload>> QuoteStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] CustomFieldFilterInput customFieldFilterInput,
        [WebhookParameter] [Display("Quote status")] [DataSource(typeof(QuoteStatusDataHandler))]
        string? status)
        => HandleWebhook<QuoteStatusChangedPayload>(webhookRequest,
            status != null
                ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) &&
                             CheckIfCustomFieldFiltersApply(customFieldFilterInput, EntityType.Quote, payload.Id)
                : payload => CheckIfCustomFieldFiltersApply(customFieldFilterInput, EntityType.Quote, payload.Id));

    [Webhook("On job status changed", typeof(JobStatusChangedHandler),
        Description = "Triggered when the status of an XTRF job is changed")]
    public Task<WebhookResponse<JobStatusChangedPayload>> JobStatusChangedHandler(WebhookRequest webhookRequest,
        [WebhookParameter] [Display("Job status")] [DataSource(typeof(JobStatusDataHandler))]
        string? status)
        => HandleWebhook<JobStatusChangedPayload>(webhookRequest,
            status != null ? payload => payload.Status.Equals(status, StringComparison.OrdinalIgnoreCase) : null);

    [Webhook("On customer created", typeof(CustomerCreatedHandler),
        Description = "Triggered when a new XTRF customer is created")]
    public Task<WebhookResponse<CustomerPayload>> CustomerCreatedHandler(WebhookRequest webhookRequest)
        => HandleWebhook<CustomerPayload>(webhookRequest);

    [Webhook("On customer updated", typeof(CustomerUpdatedHandler),
        Description = "Triggered when an XTRF customer is updated")]
    public Task<WebhookResponse<CustomerPayload>> CustomerUpdatedHandler(WebhookRequest webhookRequest, 
        [WebhookParameter] CustomFieldFilterInput customFieldFilterInput)
        => HandleWebhook<CustomerPayload>(webhookRequest, 
            payload => CheckIfCustomFieldFiltersApply(customFieldFilterInput, EntityType.Customer, payload.Id));

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

    private bool CheckIfCustomFieldFiltersApply(CustomFieldFilterInput input, EntityType entityType, string entityId)
    {
        if (input.Key == null || (input.TextValue == null && input.NumberValue == null && input.DateValue == null &&
                                  input.CheckboxValue == null))
            return true;

        var isClassic = long.TryParse(entityId, out _);
        string entityTypeEndpointPart;

        switch (entityType)
        {
            case EntityType.Project:
                entityTypeEndpointPart = "projects";
                break;
            case EntityType.Quote:
                entityTypeEndpointPart = "quotes";
                break;
            case EntityType.Customer:
                entityTypeEndpointPart = "customers";
                break;
            default:
                throw new Exception("Entity provided does not support custom fields.");
        }
        
        var endpoint = $"{(!isClassic ? "/v2" : string.Empty)}/{entityTypeEndpointPart}/{entityId}/customFields";
        
        var getCustomFieldsRequest = new XtrfRequest(endpoint, Method.Get, Creds);
        var customFields = Client.ExecuteWithErrorHandling<IEnumerable<CustomField<object>>>(getCustomFieldsRequest)
            .Result;
        var targetCustomField = customFields.FirstOrDefault(field => field.Key == input.Key);

        if (targetCustomField == null) // custom field with key provided does not exist for entity
            return true;

        var filterType = input.FilterType ?? CustomFieldFilters.Equal;
        
        if (input.TextValue != null)
        {
            if (targetCustomField.Type == CustomFieldTypes.Text || targetCustomField.Type == CustomFieldTypes.Selection)
            {
                var value = targetCustomField.Value?.ToString() ?? string.Empty;
                
                // only Equal and Contains filter types possible according to CustomFieldFilterTypeDataHandler
                switch (filterType)
                {
                    case CustomFieldFilters.Equal:
                        return input.TextValue.Equals(value, StringComparison.OrdinalIgnoreCase);
                    
                    case CustomFieldFilters.Contains:
                        return value.Contains(input.TextValue, StringComparison.OrdinalIgnoreCase);
                }
            }

            if (targetCustomField.Type == CustomFieldTypes.MultipleSelection)
            {
                var value = ((JArray)targetCustomField.Value).ToObject<IEnumerable<string>>();
                return value.Contains(input.TextValue, StringComparer.OrdinalIgnoreCase);
            }
        }

        if (input.NumberValue != null)
        {
            if (targetCustomField.Type == CustomFieldTypes.Number)
            {
                var value = Convert.ToDecimal(targetCustomField.Value);
                
                // only Equal, LessThan and MoreThan filter types possible according to CustomFieldFilterTypeDataHandler
                switch (filterType)
                {
                    case CustomFieldFilters.Equal:
                        return value == input.NumberValue;
                    
                    case CustomFieldFilters.LessThan:
                        return value < input.NumberValue;
                    
                    case CustomFieldFilters.MoreThan:
                        return value > input.NumberValue;
                }
            }
        }

        if (input.DateValue != null)
        {
            var timeZoneInfo = GetTimeZoneInfo().Result;
            
            if (targetCustomField.Type == CustomFieldTypes.Date)
            {
                var value = new LongDateTimeRepresentation(((JObject)targetCustomField.Value)["time"]?.Value<long>()).Time?
                    .ConvertFromUnixTime(timeZoneInfo);
                
                // only Equal, Before and After filter types possible according to CustomFieldFilterTypeDataHandler
                switch (filterType)
                {
                    case CustomFieldFilters.Equal:
                        return value?.Date == input.DateValue?.Date;
                    
                    case CustomFieldFilters.Before:
                        return value?.Date < input.DateValue?.Date;
                    
                    case CustomFieldFilters.After:
                        return value?.Date > input.DateValue?.Date;
                }
            }
            
            if (targetCustomField.Type == CustomFieldTypes.DateTime)
            {
                var value = new LongDateTimeRepresentation(((JObject)targetCustomField.Value)["time"]?.Value<long>()).Time?
                    .ConvertFromUnixTime(timeZoneInfo);
                
                switch (filterType)
                {
                    case CustomFieldFilters.Equal:
                        return value == input.DateValue;
                    
                    case CustomFieldFilters.Before:
                        return value < input.DateValue;
                    
                    case CustomFieldFilters.After:
                        return value > input.DateValue;
                }
            }
        }

        if (input.CheckboxValue != null)
        {
            if (targetCustomField.Type == CustomFieldTypes.Checkbox)
                return input.CheckboxValue == (bool)targetCustomField.Value!;
        }

        return true;
    }

    #endregion
}