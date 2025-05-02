using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Smart.Models.Entities;
using Apps.XTRF.Smart.Models.Requests.File;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Shared.DataSourceHandlers
{
    internal class JobTypeNameDataHandler : XtrfInvocable, IAsyncDataSourceHandler
    {
        private readonly string _projectId;

        public JobTypeNameDataHandler(InvocationContext invocationContext, [ActionParameter] CreateReceivableRequest project)
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
                .ToDictionary(job => job.StepType.Name, job => job.StepType.Name);
        }
    }
}