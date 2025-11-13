using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Shared.Actions;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Classic.DataSourceHandlers;

public class ClassicPersonDataSource(InvocationContext invocationContext)
    : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new XtrfRequest("/customers/persons/ids", Method.Get, Creds);
        //var updatedSince = DateTimeOffset.UtcNow.AddDays(-15).ToUnixTimeMilliseconds();
        //request.AddQueryParameter("updatedSince", updatedSince.ToString());
        var ids = await Client.ExecuteWithErrorHandling<List<long>>(request);
        
        var customerActions = new CustomerActions(invocationContext);
        var people = new List<Person>();
        foreach (var id in ids)
        {
            var person = await customerActions.GetContactPerson(new PersonIdentifier { PersonId = id.ToString() });
            people.Add(person);
        }

        return people
            .Where(x => context.SearchString == null ||
                        BuildReadableName(x).Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id.ToString(), BuildReadableName);
    }

    private string BuildReadableName(Person person)
    {
        return $"{person.Name} ({person.Contact?.Emails?.Primary ?? "No email"})";
    }
}