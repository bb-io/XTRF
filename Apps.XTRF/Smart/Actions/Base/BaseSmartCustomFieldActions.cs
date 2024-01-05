using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Extensions;
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

    protected override async Task UpdateTextCustomField(string entityId, string key, string value)
        => await UpdateCustomField(entityId, key, new { value });
    
    protected override async Task UpdateNumberCustomField(string entityId, string key, decimal value)
        => await UpdateCustomField(entityId, key, new { value });

    protected override async Task UpdateDateCustomField(string entityId, string key, DateTime value)
        => await UpdateCustomField(entityId, key, new { value = new { time = value.ConvertToUnixTime() } });
    
    protected override async Task UpdateCheckboxCustomField(string entityId, string key, bool value)
        => await UpdateCustomField(entityId, key, new { value });

    protected override async Task UpdateMultipleSelectionCustomField(string entityId, string key,
        IEnumerable<string> value)
        => await UpdateCustomField(entityId, key, new { value });

    private async Task UpdateCustomField(string entityId, string key, object updateBody)
    {
        var request = new XtrfRequest(string.Format(Endpoint, entityId) + $"/{key}", Method.Put, Creds)
            .WithJsonBody(updateBody);
        await Client.ExecuteWithErrorHandling(request);
    }
}