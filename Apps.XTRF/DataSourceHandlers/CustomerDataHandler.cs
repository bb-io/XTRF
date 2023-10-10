using Apps.XTRF.Api;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Responses.Models;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.DataSourceHandlers;

public class CustomerDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    public CustomerDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new XtrfRequest("/customers", Method.Get, Creds);
        var customers = await Client.ExecuteWithErrorHandling<List<SimpleCustomer>>(request);

        return customers
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}