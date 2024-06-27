using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Classic.DataSourceHandlers;

public class ClassicSpecializationDataSource(InvocationContext invocationContext, PersonIdentifier request)
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
        
        var specializationDtos = await customerPortalClient.ExecuteRequestAsync<List<SpecializationDto>>("/system/values/specializations", Method.Get, null);
        return specializationDtos
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id, x => x.Name);
    }
}

public class SpecializationDto
{
    public string Id { get; set; }
     
    public string Name { get; set; }
     
    public string LocalizedName { get; set; }
}