using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Classic.DataSourceHandlers;

public class ClassicOfficeDataSource(InvocationContext invocationContext, PersonIdentifier request)
    : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.PersonId))
        {
            throw new Exception("You must provide a Person ID first.");
        }
        
        var customerActions = new CustomerActions(InvocationContext);
        var contactPerson = await customerActions.GetContactPerson(request);
        var tokenResponse = await GetPersonAccessToken(contactPerson?.Contact?.Emails?.Primary ?? throw new Exception("Contact person has no primary email."));
        var customerPortalClient = GetCustomerPortalClient(tokenResponse.Token);    
        
        var offices = await customerPortalClient.ExecuteRequestAsync<List<FullOfficeDto>>("/offices", Method.Get, null);
        return offices
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id.ToString(), x => x.Name);
    }
}