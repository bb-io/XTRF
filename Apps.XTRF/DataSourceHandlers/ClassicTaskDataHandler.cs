using Apps.XTRF.Api;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Identifiers;
using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.DataSourceHandlers;

public class ClassicTaskDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    private readonly ProjectIdentifier _project;
    
    public ClassicTaskDataHandler(InvocationContext invocationContext, [ActionParameter] ProjectIdentifier project) 
        : base(invocationContext)
    {
        _project = project;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (_project.ProjectId == null)
            throw new Exception("Please specify project ID first.");
        
        var request = new XtrfRequest($"/projects/{_project.ProjectId}?embed=tasks", Method.Get, Creds);
        var classicProject = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        return classicProject.Tasks!
            .Where(task => context.SearchString == null
                           || task.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(task => task.Id, task => task.Name);
    }
}