using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class ServiceDataSourceHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    public ServiceDataSourceHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new XtrfRequest("/services/all", Method.Get, Creds);
        var services = await Client.ExecuteWithErrorHandling<IEnumerable<Service>>(request);
        return services
            .Where(service => context.SearchString == null 
                              || service.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(service => service.Id, service => service.Name);
    }
}