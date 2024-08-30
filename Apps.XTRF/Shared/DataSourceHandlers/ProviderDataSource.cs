using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Provider;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class ProviderDataSource(InvocationContext invocationContext)
    : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var providerActions = new ProviderActions(InvocationContext);
        var providers = await providerActions.SearchProvidersAsync(new ());
        
        return providers.Providers
            .Where(x => context.SearchString == null || x.FullName.Contains(context.SearchString))
            .ToDictionary(p => p.Id, p => p.FullName);
    }
}