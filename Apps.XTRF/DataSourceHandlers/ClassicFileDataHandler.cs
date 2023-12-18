using Apps.XTRF.Api;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Identifiers;
using Apps.XTRF.Models.Responses.ClassicTask;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.DataSourceHandlers;

public class ClassicFileDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    private readonly ClassicTaskIdentifier _task;
    
    public ClassicFileDataHandler(InvocationContext invocationContext, [ActionParameter] ClassicTaskIdentifier task) 
        : base(invocationContext)
    {
        _task = task;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (_task.TaskId == null)
            throw new Exception("Please specify task first.");
        
        var request = new XtrfRequest($"/tasks/{_task.TaskId}/files", Method.Get, Creds);
        var jobFiles = await Client.ExecuteWithErrorHandling<JobFilesResponse>(request);
        var resultFiles = new Dictionary<string, string>();

        foreach (var job in jobFiles.Jobs)
        {
            var inputFiles = job.Files.InputFiles.Select(file => new
            {
                file.Id,
                Name = $"{file.Name} ({job.IdNumber}, input)"
            }).Where(file =>
                context.SearchString == null ||
                file.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase));
            
            foreach (var file in inputFiles)
            {
                resultFiles.Add(file.Id, file.Name);
            }
            
            var outputFiles = job.Files.OutputFiles.Select(file => new
            {
                file.Id,
                Name = $"{file.Name} ({job.IdNumber}, output)"
            }).Where(file =>
                context.SearchString == null ||
                file.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase));
            
            foreach (var file in outputFiles)
            {
                resultFiles.Add(file.Id, file.Name);
            }
        }

        return resultFiles;
    }
}