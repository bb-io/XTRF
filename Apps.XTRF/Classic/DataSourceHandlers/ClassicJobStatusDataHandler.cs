using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Classic.DataSourceHandlers;

public class ClassicJobStatusDataHandler : XtrfInvocable, IAsyncDataSourceHandler
{
    private readonly JobIdentifier _jobIdentifier;
    
    public ClassicJobStatusDataHandler(InvocationContext invocationContext, [ActionParameter] JobIdentifier jobIdentifier)
        : base(invocationContext)
    {
        _jobIdentifier = jobIdentifier;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var availableStatuses = new Dictionary<string, string>
        {
            { "OPEN", "Open" },
            { "ACCEPTED", "Accepted" },
            { "STARTED", "Started" },
            { "READY", "Ready" },
            { "CANCELLED", "Cancelled" }
        };

        if (_jobIdentifier.JobId == null)
            return availableStatuses;
        
        var getJobRequest = new XtrfRequest($"/jobs/{_jobIdentifier.JobId}", Method.Get, Creds);
        var job = await Client.ExecuteWithErrorHandling<ClassicJob>(getJobRequest);

        switch (job.Status)
        {
            case "OPEN":
                return new()
                {
                    { "ACCEPTED", "Accepted" },
                    { "CANCELLED", "Cancelled" }
                };
            case "ACCEPTED":
                return new()
                {
                    { "OPEN", "Open" },
                    { "STARTED", "Started" },
                    { "CANCELLED", "Cancelled" }
                };
            case "STARTED":
                return new()
                {
                    { "ACCEPTED", "Accepted" },
                    { "READY", "Ready" },
                    { "CANCELLED", "Cancelled" }
                };
            case "READY":
                return new()
                {
                    { "STARTED", "Started" }
                };
            case "CANCELLED":
                return new()
                {
                    { "OPEN", "Open" }
                };
            case "OFFERS_SENT":
                return new()
                {
                    { "CANCELLED", "Cancelled" }
                };
            default:
                return availableStatuses;
        }
    }
}