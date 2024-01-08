using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Classic.Actions.Base;

public class BaseClassicCustomFieldActions : BaseCustomFieldActions
{
    protected BaseClassicCustomFieldActions(InvocationContext invocationContext, EntityType entityType) 
        : base(invocationContext, ApiType.Classic, entityType)
    {
    }

    protected async Task UpdateTextCustomField(string entityId, string key, string name, string value)
        => await UpdateCustomField(entityId, new CustomField<string>(CustomFieldTypes.Text, name, key, value));
    
    protected async Task UpdateNumberCustomField(string entityId, string key, string name, decimal value)
        => await UpdateCustomField(entityId, new CustomField<decimal>(CustomFieldTypes.Number, name, key, value));

    protected async Task UpdateDateCustomField(string entityId, string key, string name, DateTime value)
        => await UpdateCustomField(entityId,
            new CustomField<LongDateTimeRepresentation>(CustomFieldTypes.Date, name, key,
                new(value.ConvertToUnixTime())));
    
    protected async Task UpdateDateTimeCustomField(string entityId, string key, string name, DateTime value)
        => await UpdateCustomField(entityId,
            new CustomField<LongDateTimeRepresentation>(CustomFieldTypes.DateTime, name, key,
                new(value.ConvertToUnixTime())));
    
    protected async Task UpdateCheckboxCustomField(string entityId, string key, string name, bool value)
        => await UpdateCustomField(entityId, new CustomField<bool>(CustomFieldTypes.Checkbox, name, key, value));

    protected async Task UpdateSelectionCustomField(string entityId, string key, string name, string value)
        => await UpdateCustomField(entityId, new CustomField<string>(CustomFieldTypes.Selection, name, key, value));

    protected async Task UpdateMultipleSelectionCustomField(string entityId, string key, string name,
        IEnumerable<string> value)
        => await UpdateCustomField(entityId,
            new CustomField<IEnumerable<string>>(CustomFieldTypes.MultipleSelection, name, key, value));

    private async Task UpdateCustomField<T>(string entityId, CustomField<T> updateBody)
    {
        var request = new XtrfRequest(string.Format(Endpoint, entityId), Method.Put, Creds)
            .WithJsonBody(new[] { updateBody }, JsonConfig.Settings);
        await Client.ExecuteWithErrorHandling(request);
    }
}