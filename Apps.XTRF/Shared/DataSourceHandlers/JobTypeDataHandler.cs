using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Smart.Models.Entities;
using Apps.XTRF.Smart.Models.Requests.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class JobTypeDataHandler: XtrfInvocable, IAsyncDataSourceHandler
{
    private readonly string _projectId;
    
    public JobTypeDataHandler(InvocationContext invocationContext, [ActionParameter]CreateReceivableRequest project) 
        : base(invocationContext)
    {
        _projectId = project.ProjectId;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_projectId))
            throw new Exception("Please specify project ID first.");
        
        var jobsRequest = new XtrfRequest($"v2/projects/{_projectId}/jobs", Method.Get, Creds);
        var jobs = await Client.ExecuteWithErrorHandling<List<SmartJob>>(jobsRequest);
        
        return jobs
            .Where(job => context.SearchString == null
                         || job.StepType.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(job => job.StepType.JobTypeId, job => job.StepType.Name);
    }
}