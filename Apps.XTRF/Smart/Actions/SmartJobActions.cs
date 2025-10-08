using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Models.Entities;
using Apps.XTRF.Smart.Models.Identifiers;
using Apps.XTRF.Smart.Models.Requests.SmartJob;
using Apps.XTRF.Smart.Models.Responses.File;
using Apps.XTRF.Smart.Models.Responses.Job;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Smart.Actions;

[ActionList("Smart: jobs")]
public class SmartJobActions : BaseFileActions
{
    public SmartJobActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext, fileManagementClient)
    {
    }

    #region Get

    [Action("Smart: Get job details", Description = "Get information about a smart job")]
    public async Task<SmartJob> GetJob([ActionParameter] JobIdentifier jobIdentifier)
    {
        var request = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}", Method.Get, Creds);
        var job = await Client.ExecuteWithErrorHandling<SmartJob>(request);
        return job;
    }

    [Action("Smart: Get work files shared with job", Description = "Get work files shared with a smart job")]
    public async Task<ListFilesResponse> GetWorkFilesByJob([ActionParameter] JobIdentifier jobIdentifier)
    {
        var request = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/sharedWorkFiles", Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<IEnumerable<SmartFileXTRF>>(request);
        return new(files);
    }
    
    [Action("Smart: Get reference files shared with job", Description = "Get reference files shared with a smart job")]
    public async Task<ListFilesResponse> GetReferenceFilesByJob([ActionParameter] JobIdentifier jobIdentifier)
    {
        var request = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/sharedReferenceFiles", Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<IEnumerable<SmartFileXTRF>>(request);
        return new(files);
    }
    
    [Action("Smart: Get delivered files in job", Description = "Get delivered files in a smart job")]
    public async Task<ListFilesResponse> GetDeliveredFilesByJob([ActionParameter] JobIdentifier jobIdentifier,
        [ActionParameter] FilterLanguageOptionalIdentifier filterLanguage,
        [ActionParameter] SmartFileCategoryOptionalIdentifier category)
    {
        var request = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/delivered", Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<IEnumerable<SmartFileXTRF>>(request);
        var filteredFiles = files
            .Where(file => filterLanguage.LanguageId is null 
                           | file.LanguageRelation.Languages.Contains(filterLanguage.LanguageId))
            .Where(file => category.Category is null || file.CategoryKey == category.Category);
        return new(filteredFiles);
    }

    #endregion

    #region Post

    [Action("Smart: Upload delivered file to job", Description = "Upload a delivered file to a smart job")]
    public async Task UploadDeliveredFileToJob([ActionParameter] JobIdentifier jobIdentifier, 
        [ActionParameter] FileWrapper file,
        [ActionParameter] SmartFileCategoryIdentifier category)
    {
        var uploadFileRequest = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/delivered/upload", Method.Post, 
            Creds);
        var fileBytes = await DownloadFile(file.File);
        uploadFileRequest.AddFile("file", fileBytes, file.File.Name!.Sanitize());
        var fileIdentifier = await Client.ExecuteWithErrorHandling<FileIdentifier>(uploadFileRequest);
        
        var addFileRequest = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/delivered/add", Method.Put, Creds);
        addFileRequest.AddJsonBody(new
        {
            files = new[]
            {
                new
                {
                    category = category.Category,
                    fileId = fileIdentifier.FileId
                }
            }
        });

        await Client.ExecuteWithErrorHandling(addFileRequest);
    }

    #endregion

    #region Put

    [Action("Smart: Update job", Description = "Update a smart job, specifying only the fields that require updating")]
    public async Task<JobIdentifier> UpdateJob([ActionParameter] JobIdentifier jobIdentifier, 
        [ActionParameter] UpdateJobRequest input)
    {
        if (input.Status != null)
        {
            var updateStatusRequest = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/status", Method.Put, Creds)
                .WithJsonBody(new { status = input.Status });
            
            await Client.ExecuteWithErrorHandling(updateStatusRequest);
        }

        if (input.VendorId != null)
        {
            var assignVendorRequest = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/vendor", Method.Put, Creds)
                .WithJsonBody(new { vendorPriceProfileId = ConvertToInt64(input.VendorId, "Vendor ID") });
            
            await Client.ExecuteWithErrorHandling(assignVendorRequest);
        }
        
        if (input.StartDate != null || input.Deadline != null)
        {
            var timeZoneInfo = await GetTimeZoneInfo();
            
            var updateDatesRequest = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/dates", Method.Put, Creds)
                .WithJsonBody(new
                {
                    startDate = input.StartDate?.ConvertToUnixTime(timeZoneInfo),
                    deadline = input.Deadline?.ConvertToUnixTime(timeZoneInfo)
                }, JsonConfig.Settings);
            
            await Client.ExecuteWithErrorHandling(updateDatesRequest);
        }

        if (input.Instructions != null)
        {
            var updateInstructionsRequest = 
                new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/instructions", Method.Put, Creds)
                    .WithJsonBody(new { value = input.Instructions });

            await Client.ExecuteWithErrorHandling(updateInstructionsRequest);
        }

        return jobIdentifier;
    }
    
    [Action("Smart: Share file as reference file with job", Description = "Share a file as a reference file with a " +
                                                                          "smart job. Both the file and the job must " +
                                                                          "belong to the same project")]
    public async Task<SharedFileStatus> ShareReferenceFileWithJob([ActionParameter] JobIdentifier jobIdentifier, 
        [ActionParameter] FileIdentifier file)
    {
        var request = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/sharedReferenceFiles/share", Method.Put,
                Creds)
            .WithJsonBody(new { files = new[] { file.FileId } });
        var response = await Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
        return response.Statuses.First();
    }
    
    [Action("Smart: Share file as work file with job", Description = "Share a file as a work file with a smart job. " +
                                                                     "Both the file and the job must belong to the " +
                                                                     "same project")]
    public async Task<SharedFileStatus> ShareWorkFileWithJob([ActionParameter] JobIdentifier jobIdentifier, 
        [ActionParameter] FileIdentifier file)
    {
        var request = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/sharedWorkFiles/share", Method.Put, Creds)
            .WithJsonBody(new { files = new[] { file.FileId } });
        var response = await Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
        return response.Statuses.First();
    }
    
    [Action("Smart: Stop sharing file with job", Description = "Stop sharing a file with a smart job")]
    public async Task<SharedFileStatus> StopSharingFileWithJob([ActionParameter] JobIdentifier jobIdentifier, 
        [ActionParameter] FileIdentifier file)
    {
        var request = new XtrfRequest($"/v2/jobs/{jobIdentifier.JobId}/files/stopSharing", Method.Put, Creds)
            .WithJsonBody(new { files = new[] { file.FileId } });
        var response = await Client.ExecuteWithErrorHandling<SharedFilesResponse>(request);
        return response.Statuses.First();
    }

    #endregion
}