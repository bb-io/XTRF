using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Responses.Dictionaries;
using Apps.XTRF.Smart.Models.Entities;
using Apps.XTRF.Smart.Models.Requests.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.DataSourceHandlers;

public class JobTypeNameDataHandler(InvocationContext invocationContext) : XtrfInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var dictionaryRequest = new XtrfRequest("/dictionaries/jobType/active", Method.Get, Creds);

        var jobTypes = await Client.ExecuteWithErrorHandling<List<JobType>>(dictionaryRequest);

        if (jobTypes == null) return new Dictionary<string, string>();
        
        return jobTypes!
            .Where(jobType => context.SearchString == null
                         || jobType.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .DistinctBy(x => x.Name)
            .ToDictionary(jobType => jobType.Name, jobType => jobType.Name);
    }
}