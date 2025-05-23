using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Requests.ClassicJob;
using Apps.XTRF.Classic.Models.Responses.ClassicJob;
using Apps.XTRF.Classic.Models.Responses.ClassicTask;
using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

namespace Apps.XTRF.Classic.Actions;

[ActionList]
public class ClassicJobActions : BaseFileActions
{
    public ClassicJobActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext, fileManagementClient)
    {
    }

    #region Get

    [Action("Classic: Get job", Description = "Get information about job")]
    public async Task<JobResponse> GetJob([ActionParameter] JobIdentifier jobIdentifier)
    {
        var request = new XtrfRequest($"/jobs/{jobIdentifier.JobId}", Method.Get, Creds);
        var job = await Client.ExecuteWithErrorHandling<ClassicJob>(request);
        var timeZoneInfo = await GetTimeZoneInfo();
        return new(job, timeZoneInfo);
    }

    #endregion

    #region Post

    [Action("Classic: Upload output file for job", Description = "Upload an output file for a job")]
    public async Task<JobIdentifier> UploadOutputFile(
        [ActionParameter] JobIdentifier jobIdentifier, 
        [ActionParameter] FileWrapper file, 
        [ActionParameter][StaticDataSource(typeof(ClassicFileTypeDataSource))][Display("File category")] string? category
        )
    {
        var uploadFileRequest = new XtrfRequest("/files", Method.Post, Creds);
        var fileBytes = await DownloadFile(file.File);
        uploadFileRequest.AddFile("file", fileBytes, file.File.Name!.Sanitize());
        var uploadFileResponse = await Client.ExecuteWithErrorHandling<TokenResponse>(uploadFileRequest);
        
        var addOutputFileToJobRequest = new XtrfRequest($"/jobs/{jobIdentifier.JobId}/files/output", Method.Post, Creds)
            .WithJsonBody(new
            {
                token = uploadFileResponse.Token,
                category
            });
        await Client.ExecuteWithErrorHandling(addOutputFileToJobRequest);
        return jobIdentifier;
    }
    
    #endregion
    
    #region Put

    [Action("Classic: Update job", Description = "Update a job, specifying only the fields that require updating")]
    public async Task<JobResponse> UpdateJob([ActionParameter] JobIdentifier jobIdentifier, 
        [ActionParameter] UpdateJobRequest input)
    {
        var getJobRequest = new XtrfRequest($"/jobs/{jobIdentifier.JobId}", Method.Get, Creds);
        var job = await Client.ExecuteWithErrorHandling<ClassicJob>(getJobRequest);
        var timeZoneInfo = await GetTimeZoneInfo();

        if (input.Status != null)
        {
            var updateJobStatusRequest = new XtrfRequest($"/jobs/{jobIdentifier.JobId}/status", Method.Put, Creds)
                .WithJsonBody(new { status = input.Status });
            await Client.ExecuteWithErrorHandling(updateJobStatusRequest);
            job.Status = input.Status;
        }

        if (input.VendorId != null)
        {
            var updateVendorRequest = new XtrfRequest($"/jobs/{jobIdentifier.JobId}/vendor", Method.Put, Creds)
                .WithJsonBody(new { vendorPriceProfileId = ConvertToInt64(input.VendorId, "Vendor ID") });
            await Client.ExecuteWithErrorHandling(updateVendorRequest);
            job.VendorId = input.VendorId;
        }

        if (input.StartDate != null || input.Deadline != null)
        {
            var jsonBody = new
            {
                startDate = input.StartDate == null
                    ? job.Dates.StartDate
                    : input.StartDate?.ConvertToUnixTime(timeZoneInfo),
                deadline = input.Deadline == null ? job.Dates.Deadline : input.Deadline?.ConvertToUnixTime(timeZoneInfo)
            };

            var updateDatesRequest =
                new XtrfRequest($"/jobs/{jobIdentifier.JobId}/dates", Method.Put, Creds).WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateDatesRequest);

            job.Dates.StartDate = jsonBody.startDate;
            job.Dates.Deadline = jsonBody.deadline;
        }

        if (input.InstructionFromClient != null || input.InstructionForVendor != null ||
            input.InternalInstruction != null || input.PaymentNoteForVendor != null)
        {
            var jsonBody = new
            {
                fromClient = input.InstructionFromClient ?? job.Instructions.FromClient,
                forVendor = input.InstructionForVendor ?? job.Instructions.ForVendor,
                Internal = input.InternalInstruction ?? job.Instructions.Internal,
                paymentNoteForVendor = input.PaymentNoteForVendor ?? job.Instructions.PaymentNoteForVendor
            };

            var updateInstructionsRequest = new XtrfRequest($"/jobs/{jobIdentifier.JobId}/instructions", Method.Put, Creds)
                .WithJsonBody(jsonBody, JsonConfig.Settings);
            await Client.ExecuteWithErrorHandling(updateInstructionsRequest);

            job.Instructions.FromClient = jsonBody.fromClient;
            job.Instructions.ForVendor = jsonBody.forVendor;
            job.Instructions.Internal = jsonBody.Internal;
            job.Instructions.PaymentNoteForVendor = jsonBody.paymentNoteForVendor;
        }

        return new(job, timeZoneInfo);
    }

    #endregion
}