using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class BaseQuoteCustomFieldDataHandler(
    string quoteId,
    ApiType apiType,
    InvocationContext invocationContext)
    : XtrfInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        string endpoint = $"{(apiType == ApiType.Smart ? "/v2" : string.Empty)}/quotes/{quoteId}/customFields";
        var request = new XtrfRequest(string.Format(endpoint, quoteId), Method.Get, Creds);
        var result = await Client.ExecuteWithErrorHandling<IEnumerable<CustomField>>(request);
        return result.Select(x => new DataSourceItem(x.Key, $"{x.Name} - {x.Type.ToLower()}"));
    }
}
