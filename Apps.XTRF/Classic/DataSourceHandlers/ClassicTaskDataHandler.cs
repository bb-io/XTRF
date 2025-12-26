using RestSharp;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Classic.Models.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Classic.DataSourceHandlers;

public class ClassicTaskDataHandler(
    InvocationContext invocationContext,
    [ActionParameter] ProjectIdentifier project)
    : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    private readonly string? _projectId = project.ProjectId;

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(_projectId))
        {
            throw new ArgumentException("Please fill in Project ID first");
        }
        
        var request = new XtrfRequest($"/projects/{_projectId}?embed=tasks", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        
        return project.Tasks
            .Where(task => context.SearchString == null 
                          || task.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(task => task.Id, task => task.Name);
    }
}