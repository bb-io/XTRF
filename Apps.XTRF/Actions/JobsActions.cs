using Apps.XTRF.Api;
using Apps.XTRF.Extensions;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Requests;
using Apps.XTRF.Models.Requests.Job;
using Apps.XTRF.Models.Responses;
using Apps.XTRF.Models.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Actions;

[ActionList]
public class JobsActions : XtrfInvocable
{
    public JobsActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get job details", Description = "Get all information of a specific job")]
    public async Task<JobDTO> GetJob(
        IEnumerable<AuthenticationCredentialsProvider> Creds,
        [ActionParameter] [Display("Job ID")] string jobId)
    {
        var request = new XtrfRequest("/v2/jobs/" + jobId, Method.Get, Creds);
        var jobResult = await Client.ExecuteWithErrorHandling<JobResponse>(request);

        return new(jobResult);
    }

    [Action("Get work files shared with a job", Description = "Get all work files shared with a specific job")]
    public async Task<GetFilesResponse> GetWorkFilesByJob([ActionParameter] [Display("Job ID")] string jobId)
    {
        var endpoint = "/v2/jobs/" + jobId + "/files/sharedWorkFiles";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return new()
        {
            Files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request)
        };
    }

    [Action("Get reference files shared with a job",
        Description = "Get all reference files shared with a specific job")]
    public async Task<GetFilesResponse> GetReferenceFilesByJob([ActionParameter] [Display("Job ID")] string jobId)
    {
        var endpoint = "/v2/jobs/" + jobId + "/files/sharedReferenceFiles";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return new()
        {
            Files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request)
        };
    }

    [Action("Get delivered files in a job", Description = "Get all delivered files in a specific job")]
    public async Task<GetFilesResponse> GetDeliveredFilesByJob([ActionParameter] GetDeliveredJobFilesRequest input)
    {
        var endpoint = "/v2/jobs/" + input.JobId + "/files/delivered";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        var files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request);

        return new()
        {
            Files = files.Where(x => input.LanguageId is null || x.Languages.Contains(int.Parse(input.LanguageId)))
                .Where(x => input.Category is null || x.CategoryKey == input.Category)
        };
    }

    [Action("Upload a delivered file to a job", Description = "Upload a delivered file to a specific job")]
    public async Task UploadDeliveredFileToJob([ActionParameter] UploadFileToJobRequest input)
    {
        var uploadEndpoint = "/v2/jobs/" + input.JobId + "/files/delivered/upload";
        var uploadRequest = new XtrfRequest(uploadEndpoint, Method.Post, Creds);
        uploadRequest.AddFile("file", input.File.Bytes, input.FileName ?? input.File.Name);

        var outputFileId = (await Client.ExecuteWithErrorHandling<UploadFileResponse>(uploadRequest)).FileId;

        var addEndpoint = "/v2/jobs/" + input.JobId + "/files/delivered/add";
        var addRequest = new XtrfRequest(addEndpoint, Method.Put, Creds);
        addRequest.AddJsonBody(new
        {
            files = new[]
            {
                new
                {
                    category = input.Category,
                    fileId = outputFileId
                }
            }
        });

        await Client.ExecuteWithErrorHandling(addRequest);
    }

    [Action("Assign vendor to a job", Description = "Assign vendor to a specific job")]
    public Task AssignVendorToJob(
        [ActionParameter] [Display("Job ID")] string jobId,
        [ActionParameter] [Display("Vendor ID")]
        string vendorId)
    {
        if (!int.TryParse(vendorId, out var intVendorId))
            throw new("Vendor ID must be a number");

        var endpoint = "/v2/jobs/" + jobId + "/vendor";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            vendorPriceProfileId = intVendorId
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update instructions for a job", Description = "Update instructions for a specific job")]
    public Task UpdateInstructionsForJob(
        [ActionParameter] [Display("Job ID")] string jobId,
        [ActionParameter] [Display("Instructions")]
        string instructions)
    {
        var endpoint = "/v2/jobs/" + jobId + "/instructions";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = instructions
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update dates of a job", Description = "Update dates of a given job")]
    public Task UpdateDatesOfJob([ActionParameter] UpdateJobDatesRequest input)
    {
        var endpoint = "/v2/jobs/" + input.JobId + "/dates";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            startDate = input.StartDate.ConvertToUnixTime(),
            deadline = input.Deadline.ConvertToUnixTime()
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Share file as referenced with a job", Description = "Share file as referenced with a specific job")]
    public Task<SharedFilesResponse> ShareReferencedFileWithJob([ActionParameter] ShareFileWithJobRequest input)
    {
        var endpoint = "/v2/jobs/" + input.JobId + "/files/sharedReferenceFiles/share";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            files = new[] { input.FileId }
        });

        return Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
    }

    [Action("Share file as work files with a job", Description = "Share file as work files with a specific job")]
    public Task<SharedFilesResponse> ShareWorkFileWithJob([ActionParameter] ShareFileWithJobRequest input)
    {
        var endpoint = "/v2/jobs/" + input.JobId + "/files/sharedWorkFiles/share";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            files = new[] { input.FileId }
        });

        return Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
    }

    [Action("Stop sharing file with a job", Description = "Stop sharing file with a specific job")]
    public Task<SharedFilesResponse> StopSharingFileWithJob([ActionParameter] ShareFileWithJobRequest input)
    {
        var endpoint = "/v2/jobs/" + input.JobId + "/files/stopSharing";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            files = new[] { input.FileId }
        });

        return Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
    }
}