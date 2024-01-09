using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.XTRF.Shared.Actions.Base;

public abstract class BaseCustomFieldActions : XtrfInvocable
{
    protected readonly string Endpoint;
    
    protected BaseCustomFieldActions(InvocationContext invocationContext, ApiType apiType, EntityType entityType) 
        : base(invocationContext)
    {
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
        
        Endpoint = $"{(apiType == ApiType.Smart ? "/v2" : string.Empty)}/{entityTypeEndpointPart}/{{0}}/customFields";
    }

    #region Get

    protected async Task<IEnumerable<CustomField>> ListCustomFields(string entityId)
    {
        var request = new XtrfRequest(string.Format(Endpoint, entityId), Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<IEnumerable<CustomField>>(request);
    }
    
    protected async Task<CustomField<string>> GetTextCustomField(string entityId, string key)
    {
        var field = await GetCustomField(entityId, key);
        return new(field.Type, field.Name, field.Key, field.Value?.ToString());
    }
    
    protected async Task<CustomField<decimal?>> GetNumberCustomField(string entityId, string key)
    {
        var field = await GetCustomField(entityId, key);
        return new(field.Type, field.Name, field.Key, Convert.ToDecimal(field.Value));
    }
    
    protected async Task<CustomField<DateTime?>> GetDateCustomField(string entityId, string key)
    {
        var field = await GetCustomField(entityId, key);
        var value = new LongDateTimeRepresentation(((JObject)field.Value)["time"]?.Value<long>());
        return new(field.Type, field.Name, field.Key, value.Time?.ConvertFromUnixTime());
    }
    
    protected async Task<CustomField<bool?>> GetCheckboxCustomField(string entityId, string key)
    {
        var field = await GetCustomField(entityId, key);
        return new(field.Type, field.Name, field.Key, (bool)field.Value);
    }
    
    protected async Task<CustomField<IEnumerable<string>>> GetMultipleSelectionCustomField(string entityId, string key)
    {
        var field = await GetCustomField(entityId, key);
        var value = ((JArray)field.Value).ToObject<IEnumerable<string>>();
        return new(field.Type, field.Name, field.Key, value);
    }
    
    private async Task<CustomField<object>> GetCustomField(string entityId, string key)
    {
        var request = new XtrfRequest(string.Format(Endpoint, entityId), Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<IEnumerable<CustomField<object>>>(request);
        var customField = response.FirstOrDefault(field => field.Key == key);

        if (customField == null)
            throw new Exception($"No custom field exists with {key} key.");
        
        return customField;
    }

    #endregion

    #region Put

    protected abstract Task UpdateCustomField<T>(string entityId, string key, T value);

    #endregion
}