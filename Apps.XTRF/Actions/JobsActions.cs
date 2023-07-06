using Apps.XTRF.Requests;
using Apps.XTRF.Responses;
using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF.Actions
{
    [ActionList]
    public class JobsActions
    {
        [Action("Get job details", Description = "Get all information of a specific job")]
        public JobDTO GetJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId, Method.Get, authenticationCredentialsProviders);
            var jobResult = client.ExecuteRequest<JobResponse>(request);
            return ExtensionMethods.MapJobResponseToDTO(jobResult);
        }

        [Action("Get work files shared with a job", Description = "Get all work files shared with a specific job")]
        public GetFilesResponse GetWorkFilesByJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/files/sharedWorkFiles", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.ExecuteRequest<List<FileXTRF>>(request)
            };
        }

        [Action("Get reference files shared with a job", Description = "Get all reference files shared with a specific job")]
        public GetFilesResponse GetReferenceFilesByJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/files/sharedReferenceFiles", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.ExecuteRequest<List<FileXTRF>>(request)
            };
        }

        [Action("Get delivered files in a job", Description = "Get all delivered files in a specific job")]
        public GetFilesResponse GetDeliveredFilesByJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/files/delivered", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.ExecuteRequest<List<FileXTRF>>(request)
            };
        }

        [Action("Upload a delivered file to a job", Description = "Upload a delivered file to a specific job")]
        public void UploadDeliveredFileToJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] UploadFileToJobRequest input)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var uploadRequest = new XtrfRequest("/v2/jobs/" + input.JobId + "/files/delivered/upload", Method.Post, authenticationCredentialsProviders);
            uploadRequest.AddFile("file", input.File, input.FileName);
            var outputFileId = client.ExecuteRequest<UploadFileResponse>(uploadRequest).FileId;

            var addRequest = new XtrfRequest("/v2/jobs/" + input.JobId + "/files/delivered/add", Method.Put, authenticationCredentialsProviders);
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

            client.ExecuteRequest<object>(addRequest);
        }

        [Action("Assign vendor to a job", Description = "Assign vendor to a specific job")]
        public void AssignVendorToJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId, [ActionParameter] int vendorId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/vendor", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                vendorPriceProfileId = vendorId
            });
            client.ExecuteRequest<object>(request);
        }

        [Action("Update instructions for a job", Description = "Update instructions for a specific job")]
        public void UpdateInstructionsForJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId, [ActionParameter] string instructions)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/instructions", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = instructions
            });
            client.ExecuteRequest<object>(request);
        }

        [Action("Update dates of a job", Description = "Update dates of a given job")]
        public void UpdateDatesOfJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] UpdateJobDatesRequest input)
        {

            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + input.JobId + "/dates", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                startDate =input.StartDate.ConvertToUnixTime(),
                deadline = input.Deadline.ConvertToUnixTime()
            });
            client.ExecuteRequest<object>(request);
        }

        [Action("Share file as referenced with a job", Description = "Share file as referenced with a specific job")]
        public SharedFilesResponse ShareReferencedFileWithJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] ShareFileWithJobRequest input)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + input.JobId + "/files/sharedReferenceFiles/share", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                files = new[] { input.FileId }
            });
            return client.ExecuteRequest<SharedFilesResponse>(request);
        }

        [Action("Share file as work files with a job", Description = "Share file as work files with a specific job")]
        public SharedFilesResponse ShareWorkFileWithJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] ShareFileWithJobRequest input)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + input.JobId + "/files/sharedWorkFiles/share", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                files = new[] { input.FileId }
            });
            return client.ExecuteRequest<SharedFilesResponse>(request);

        }

        [Action("Stop sharing file with a job", Description = "Stop sharing file with a specific job")]
        public SharedFilesResponse StopSharingFileWithJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] ShareFileWithJobRequest input)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + input.JobId + "/files/stopSharing", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                files = new[] { input.FileId }
            });
            return client.ExecuteRequest<SharedFilesResponse>(request);

        }
    }
}
