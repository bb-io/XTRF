using Apps.XTRF.Api;
using Apps.XTRF.Constants;
using Apps.XTRF.Extensions;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models;
using Apps.XTRF.Models.Identifiers;
using Apps.XTRF.Models.Requests.ClassicJob;
using Apps.XTRF.Models.Responses.ClassicJob;
using Apps.XTRF.Models.Responses.ClassicTask;
using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Actions;

[ActionList]
public class ClassicJobActions : XtrfInvocable
{
    public ClassicJobActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #region Get

    [Action("Classic: Get job", Description = "Get information about job")]
    public async Task<JobResponse> GetJob([ActionParameter] JobIdentifier job)
    {
        var request = new XtrfRequest($"/jobs/{job.JobId}", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ClassicJob>(request);
        return new(response);
    }

    #endregion

    #region Post

    [Action("Classic: Upload output file for job", Description = "Upload an output file for a job")]
    public async Task<JobIdentifier> UploadOutputFile([ActionParameter] JobIdentifier job, 
        [ActionParameter] FileWrapper file)
    {
        var uploadFileRequest = new XtrfRequest("/files", Method.Post, Creds);
        uploadFileRequest.AddFile("file", file.File.Bytes, file.File.Name);
        var uploadFileResponse = await Client.ExecuteWithErrorHandling<TokenResponse>(uploadFileRequest);
        
        var addOutputFileToJobRequest = new XtrfRequest($"/jobs/{job.JobId}/files/output", Method.Post, Creds)
            .WithJsonBody(new
            {
                token = uploadFileResponse.Token
            });
        await Client.ExecuteWithErrorHandling(addOutputFileToJobRequest);
        return job;
    }

    #endregion
    
    #region Put

    [Action("Classic: Update job", Description = "Update a job, specifying only the fields that require updating")]
    public async Task<JobResponse> UpdateJob([ActionParameter] JobIdentifier job, 
        [ActionParameter] UpdateJobRequest input)
    {
        var getJobRequest = new XtrfRequest($"/jobs/{job.JobId}", Method.Get, Creds);
        var targetJob = await Client.ExecuteWithErrorHandling<ClassicJob>(getJobRequest);

        if (input.Status != null)
        {
            var updateJobStatusRequest = new XtrfRequest($"/jobs/{job.JobId}/status", Method.Put, Creds)
                .WithJsonBody(new { status = input.Status });
            await Client.ExecuteWithErrorHandling(updateJobStatusRequest);
            targetJob.Status = input.Status;
        }

        if (input.VendorId != null)
        {
            var updateVendorRequest = new XtrfRequest($"/jobs/{job.JobId}/vendor", Method.Put, Creds)
                .WithJsonBody(new { vendorPriceProfileId = ConvertToInt64(input.VendorId, "Vendor ID") });
            await Client.ExecuteWithErrorHandling(updateVendorRequest);
            targetJob.VendorId = input.VendorId;
        }

        if (input.StartDate != null || input.Deadline != null)
        {
            var jsonBody = new
            {
                startDate = input.StartDate == null ? targetJob.Dates.StartDate : input.StartDate?.ConvertToUnixTime(),
                deadline = input.Deadline == null ? targetJob.Dates.Deadline : input.Deadline?.ConvertToUnixTime(),
            };

            var updateDatesRequest =
                new XtrfRequest($"/jobs/{job.JobId}/dates", Method.Put, Creds).WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateDatesRequest);

            targetJob.Dates.StartDate = jsonBody.startDate;
            targetJob.Dates.Deadline = jsonBody.deadline;
        }

        if (input.InstructionFromClient != null || input.InstructionForVendor != null ||
            input.InternalInstruction != null || input.PaymentNoteForVendor != null)
        {
            var jsonBody = new
            {
                fromClient = input.InstructionFromClient ?? targetJob.Instructions.FromClient,
                forVendor = input.InstructionForVendor ?? targetJob.Instructions.ForVendor,
                Internal = input.InternalInstruction ?? targetJob.Instructions.Internal,
                paymentNoteForVendor = input.PaymentNoteForVendor ?? targetJob.Instructions.PaymentNoteForVendor
            };

            var updateInstructionsRequest = new XtrfRequest($"/jobs/{job.JobId}/instructions", Method.Put, Creds)
                .WithJsonBody(jsonBody, JsonConfig.Settings);
            await Client.ExecuteWithErrorHandling(updateInstructionsRequest);

            targetJob.Instructions.FromClient = jsonBody.fromClient;
            targetJob.Instructions.ForVendor = jsonBody.forVendor;
            targetJob.Instructions.Internal = jsonBody.Internal;
            targetJob.Instructions.PaymentNoteForVendor = jsonBody.paymentNoteForVendor;
        }

        return new(targetJob);
    }

    #endregion
}