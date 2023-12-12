using Apps.XTRF.Api;
using Apps.XTRF.Extensions;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Identifiers;
using Apps.XTRF.Models.Requests.Job;
using Apps.XTRF.Models.Responses.Entities;
using Apps.XTRF.Models.Responses.File;
using Apps.XTRF.Models.Responses.Job;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
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
    public async Task<Job> GetJob([ActionParameter] JobIdentifier job)
    {
        var request = new XtrfRequest($"/v2/jobs/{job.JobId}", Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<Job>(request);
    }

    [Action("Get work files shared with a job", Description = "Get all work files shared with a specific job")]
    public async Task<GetFilesResponse> GetWorkFilesByJob([ActionParameter] JobIdentifier job)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/files/sharedWorkFiles";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return new()
        {
            Files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request)
        };
    }

    [Action("Get reference files shared with a job",
        Description = "Get all reference files shared with a specific job")]
    public async Task<GetFilesResponse> GetReferenceFilesByJob([ActionParameter] JobIdentifier job)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/files/sharedReferenceFiles";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return new()
        {
            Files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request)
        };
    }

    [Action("Get delivered files in a job", Description = "Get all delivered files in a specific job")]
    public async Task<GetFilesResponse> GetDeliveredFilesByJob([ActionParameter] JobIdentifier job,
        [ActionParameter] GetDeliveredJobFilesRequest input)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/files/delivered";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request);
    
        return new()
        {
            Files = files
                .Where(file => input.LanguageId is null || file.LanguageRelation.Languages.Contains(input.LanguageId))
                .Where(file => input.Category is null || file.CategoryKey == input.Category)
        };
    }

    [Action("Upload a delivered file to a job", Description = "Upload a delivered file to a specific job")]
    public async Task UploadDeliveredFileToJob([ActionParameter] JobIdentifier job,
        [ActionParameter] UploadFileToJobRequest input)
    {
        var uploadEndpoint = $"/v2/jobs/{job.JobId}/files/delivered/upload";
        var uploadRequest = new XtrfRequest(uploadEndpoint, Method.Post, Creds);
        uploadRequest.AddFile("file", input.File.Bytes, input.FileName ?? input.File.Name);

        var outputFileId = (await Client.ExecuteWithErrorHandling<UploadFileResponse>(uploadRequest)).FileId;

        var addEndpoint = $"/v2/jobs/{job.JobId}/files/delivered/add";
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
    public async Task AssignVendorToJob([ActionParameter] JobIdentifier job,
        [ActionParameter] VendorIdentifier vendor)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/vendor";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new { vendorPriceProfileId = long.Parse(vendor.VendorId) });
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update instructions for a job", Description = "Update instructions for a specific job")]
    public async Task UpdateInstructionsForJob([ActionParameter] JobIdentifier job,
        [ActionParameter] [Display("Instructions")] string instructions)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/instructions";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new { value = instructions });
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update dates of a job", Description = "Update dates of a given job")]
    public async Task UpdateDatesOfJob([ActionParameter] JobIdentifier job,
        [ActionParameter] UpdateJobDatesRequest input)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/dates";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            startDate = input.StartDate.ConvertToUnixTime(),
            deadline = input.Deadline.ConvertToUnixTime()
        });

        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Share file as referenced with a job", Description = "Share file as referenced with a specific job")]
    public async Task<SharedFilesResponse> ShareReferencedFileWithJob([ActionParameter] JobIdentifier job, 
        [ActionParameter] FileIdentifier file)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/files/sharedReferenceFiles/share";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new { files = new[] { file.FileId } });
        return await Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
    }

    [Action("Share file as work files with a job", Description = "Share file as work files with a specific job")]
    public async Task<SharedFilesResponse> ShareWorkFileWithJob([ActionParameter] JobIdentifier job, 
        [ActionParameter] FileIdentifier file)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/files/sharedWorkFiles/share";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new { files = new[] { file.FileId } });
        return await Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
    }

    [Action("Stop sharing file with a job", Description = "Stop sharing file with a specific job")]
    public async Task<SharedFilesResponse> StopSharingFileWithJob([ActionParameter] JobIdentifier job, 
        [ActionParameter] FileIdentifier file)
    {
        var endpoint = $"/v2/jobs/{job.JobId}/files/stopSharing";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new { files = new[] { file.FileId } });
        return await Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
    }
}