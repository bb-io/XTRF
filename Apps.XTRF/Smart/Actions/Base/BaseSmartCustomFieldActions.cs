using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Smart.Actions.Base;

public abstract class BaseSmartCustomFieldActions : BaseCustomFieldActions
{
    protected BaseSmartCustomFieldActions(InvocationContext invocationContext, EntityType entityType) 
        : base(invocationContext, ApiType.Smart, entityType)
    {
    }

    protected override async Task UpdateCustomField<T>(string entityId, string key, T value)
    {
        var request = new XtrfRequest(string.Format(Endpoint, entityId) + $"/{key}", Method.Put, Creds)
            .WithJsonBody(new { value }, JsonConfig.Settings);
        await Client.ExecuteWithErrorHandling(request);
    }
}