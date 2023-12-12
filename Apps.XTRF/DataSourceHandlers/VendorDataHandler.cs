using Apps.XTRF.Api;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.DataSourceHandlers;

public class VendorDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    public VendorDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var getProviderIdsRequest = new XtrfRequest("/providers/ids", Method.Get, Creds);
        var providerIds = await Client.ExecuteWithErrorHandling<IEnumerable<long>>(getProviderIdsRequest);
        var providerDictionary = new Dictionary<string, string>();

        foreach (var providerId in providerIds)
        {
            var getProviderRequest = new XtrfRequest($"/providers/{providerId}", Method.Get, Creds);
            var provider = await Client.ExecuteWithErrorHandling<Provider>(getProviderRequest);
            
            if (context.SearchString == null || provider.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase)) 
                providerDictionary.Add(provider.Id, provider.Name);

            if (providerDictionary.Count == 20)
                break;
        }

        return providerDictionary;
    }
}