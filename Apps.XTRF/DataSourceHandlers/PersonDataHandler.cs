using Apps.XTRF.Api;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.DataSourceHandlers;

public class PersonDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    public PersonDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var getPersonIdsRequest = new XtrfRequest("/customers/persons/ids", Method.Get, Creds);
        var personIds = await Client.ExecuteWithErrorHandling<IEnumerable<long>>(getPersonIdsRequest);
        var personDictionary = new Dictionary<string, string>();

        foreach (var personId in personIds)
        {
            var getPersonRequest = new XtrfRequest($"/customers/persons/{personId}", Method.Get, Creds);
            var person = await Client.ExecuteWithErrorHandling<Person>(getPersonRequest);
            var name = person.Name + (!string.IsNullOrWhiteSpace(person.LastName) ? $" {person.LastName}" : string.Empty);
            
            if (context.SearchString == null || name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase)) 
                personDictionary.Add(person.Id, name);

            if (personDictionary.Count == 20)
                break;
        }

        return personDictionary;
    }
}