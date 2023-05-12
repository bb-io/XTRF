using Apps.XTRF.Requests;
using Apps.XTRF.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Actions
{
    [ActionList]
    public class JobsActions
    {
        [Action("Get job details", Description = "Get all information of a specific job")]
        public Job GetJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId, Method.Get, authenticationCredentialsProviders);
            return client.Get<Job>(request);
        }

        [Action("Get work files shared with a job", Description = "Get all work files shared with a specific job")]
        public GetFilesResponse GetWorkFilesByJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/files/sharedWorkFiles", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.Get<List<FileXTRF>>(request)
            };
        }

        [Action("Get reference files shared with a job", Description = "Get all reference files shared with a specific job")]
        public GetFilesResponse GetReferenceFilesByJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/files/sharedReferenceFiles", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.Get<List<FileXTRF>>(request)
            };
        }

        [Action("Get delivered files in a job", Description = "Get all delivered files in a specific job")]
        public GetFilesResponse GetDeliveredFilesByJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string jobId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/jobs/" + jobId + "/files/delivered", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.Get<List<FileXTRF>>(request)
            };
        }

        [Action("Upload a delivered file to a job", Description = "Upload a delivered file to a specific job")]
        public void UploadDeliveredFileToJob(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] UploadFileToJobRequest input)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var uploadRequest = new XtrfRequest("/v2/jobs/" + input.JobId + "/files/delivered/upload", Method.Post, authenticationCredentialsProviders);
            uploadRequest.AddFile("file", input.File, input.FileName);
            var outputFileId = client.Post<UploadFileResponse>(uploadRequest).FileId;

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

            client.Execute(addRequest);

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
            client.Execute(request);
        }
    }
}
