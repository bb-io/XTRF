using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Macros;
using Apps.XTRF.Shared.Models.Requests.Provider;
using Apps.XTRF.Shared.Models.Responses.Macros;
using Apps.XTRF.Shared.Models.Responses.Provider;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.Actions;

[ActionList]
public class ProviderActions(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
{
    [Action("Search providers", Description = "Search for providers based on the given criteria")]
    public async Task<ProviderSearchResponse> SearchProvidersAsync([ActionParameter] ProviderSearchRequest request)
    {
        var xtrfRequest = new XtrfRequest("/providers/ids", Method.Get, Creds);
        var ids = await Client.ExecuteWithErrorHandling<List<int>>(xtrfRequest);
        
        var response = new ProviderSearchResponse();
        foreach (var id in ids)
        {
            var provider = await GetProviderAsync(new ProviderIdentifier
            {
                ProviderId = id.ToString()
            });
            
            if(request.IdNumber != null && !provider.IdNumber.Equals(request.IdNumber))
            {
                continue;
            }
            
            response.Providers.Add(provider);
        }
        
        return response;
    }
    
    [Action("Get provider", Description = "Get information about specific provider")]
    public async Task<ProviderResponse> GetProviderAsync([ActionParameter] ProviderIdentifier identifier)
    {
        var request = new XtrfRequest($"/providers/{identifier.ProviderId}?embed=persons", Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<ProviderResponse>(request);
    }
    
    [Action("Delete provider", Description = "Delete provider with the given ID")]
    public async Task DeleteProviderAsync([ActionParameter] ProviderIdentifier identifier)
    {
        var request = new XtrfRequest($"/providers/{identifier.ProviderId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Send invitation to provider", Description = "Send invitation to provider with the given ID")]
    public async Task<SendInvitationResponse> SendInvitationToProviderAsync([ActionParameter] ProviderIdentifier identifier)
    {
        var request = new XtrfRequest($"/providers/{identifier.ProviderId}/notification/invitation", Method.Post, Creds);
        return await Client.ExecuteWithErrorHandling<SendInvitationResponse>(request);
    }



    [Action("Run macros", Description = "Run a macro by ID, optionally passing a list of item IDs")]
    public async Task<RunMacroResponse> RunMacroAsync([ActionParameter] RunMacroRequest request)
    {
        var items = (request.Items ?? Enumerable.Empty<string>())
       .Select(int.Parse)
       .ToList();
        var body = new
        {
            ids = items,
            async = request.Async ?? true
        };

        var xtrfRequest = new XtrfRequest($"/macros/{request.MacroId}/run", Method.Post, Creds);
        xtrfRequest.AddJsonBody(body);

        return await Client.ExecuteWithErrorHandling<RunMacroResponse>(xtrfRequest);
    }
}