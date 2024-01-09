using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Classic.Actions.Base;

public abstract class BaseClassicCustomFieldActions : BaseCustomFieldActions
{
    protected BaseClassicCustomFieldActions(InvocationContext invocationContext, EntityType entityType) 
        : base(invocationContext, ApiType.Classic, entityType)
    {
    }

    protected override async Task UpdateCustomField<T>(string entityId, string key, T value)
    {
        var customFields = await ListCustomFields(entityId);
        var customField = customFields.First(field => field.Key == key);

        var request = new XtrfRequest(string.Format(Endpoint, entityId), Method.Put, Creds)
            .WithJsonBody(new[] { new CustomField<T>(customField.Type, customField.Name, key, value) },
                JsonConfig.Settings);
        await Client.ExecuteWithErrorHandling(request);
    }
}