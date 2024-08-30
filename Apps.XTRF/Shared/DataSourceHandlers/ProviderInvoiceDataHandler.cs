using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Responses.Invoices;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class ProviderInvoiceDataHandler(InvocationContext invocationContext)
    : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var providerActions = new ProviderInvoiceActions(InvocationContext, null!);
        var providers = await providerActions.SearchProviderInvoicesAsync(new ());
        
        return providers.Invoices
            .Where(x => context.SearchString == null || BuildReadableName(x).Contains(context.SearchString))
            .ToDictionary(p => p.Id, BuildReadableName);
    }
    
    private string BuildReadableName(ProviderInvoiceResponse providerInvoice)
    {
        return $"[{providerInvoice.Id}] {providerInvoice.Status} ({providerInvoice.TotalGross}$)";
    }
}