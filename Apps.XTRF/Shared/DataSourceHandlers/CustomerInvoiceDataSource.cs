using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class CustomerInvoiceDataSource(InvocationContext invocationContext)
    : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var actions = new CustomerInvoiceActions(InvocationContext, null!);
        var providers = await actions.SearchCustomerInvoicesAsync(new ());
        
        return providers.Invoices
            .Where(x => context.SearchString == null || BuildReadableName(x).Contains(context.SearchString))
            .ToDictionary(p => p.Id, BuildReadableName);
    }
    
    private string BuildReadableName(CustomerInvoiceResponse invoice)
    {
        return $"[{invoice.Id}] {invoice.Status} ({invoice.TotalGross}$)";
    }
}