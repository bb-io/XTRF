using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class ProjectContactDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    private readonly ProjectIdentifier _project;
    
    public ProjectContactDataHandler(InvocationContext invocationContext, [ActionParameter] ProjectIdentifier project) 
        : base(invocationContext)
    {
        _project = project;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (_project.ProjectId == null)
            throw new Exception("Please specify project ID first.");

        string customerId;
        
        if (long.TryParse(_project.ProjectId, out _))
        {
            var request = new XtrfRequest($"/projects/{_project.ProjectId}", Method.Get, Creds);
            var classicProject = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
            customerId = classicProject.CustomerId;
        }
        else
        {
            var request = new XtrfRequest($"/v2/projects/{_project.ProjectId}", Method.Get, Creds);
            var smartProject = await Client.ExecuteWithErrorHandling<Project>(request);
            customerId = smartProject.ClientId;
        }

        var getCustomerRequest = new XtrfRequest($"/customers/{customerId}?embed=persons", Method.Get, Creds);
        var customer = await Client.ExecuteWithErrorHandling<Customer>(getCustomerRequest);
        return customer.Persons
            .Select(person => new
            {
                person.Id,
                FullName = person.Name + (string.IsNullOrWhiteSpace(person.LastName) ? "" : " " + person.LastName)
            })
            .Where(person => context.SearchString == null
                             || person.FullName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(person => person.Id, person => person.FullName);
    }
}