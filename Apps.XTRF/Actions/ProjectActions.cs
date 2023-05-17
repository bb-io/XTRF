using Apps.XTRF.Requests;
using Apps.XTRF.Responses;
using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Actions
{
    [ActionList]
    public class ProjectActions
    {
        [Action("Get project details", Description = "Get all information of a specific project")]
        public Project GetProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] int id)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + id, Method.Get, authenticationCredentialsProviders);
            return client.Get<Project>(request);
        }

        [Action("Create new project", Description = "Create a new project")]
        public Project CreateProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] SimpleProject project)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(project);
            return client.Post<Project>(request);
        }

        [Action("Get jobs in a project", Description = "Get all jobs of a specific project")]
        public GetJobsResponse GetJobsByProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/jobs", Method.Get, authenticationCredentialsProviders);
            return new GetJobsResponse()
            {
                Jobs = client.Get<List<Job>>(request)
            };
        }

        [Action("Get files in a project", Description = "Get all files of a specific project")]
        public GetFilesResponse GetFilesByProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/files", Method.Get, authenticationCredentialsProviders);
            return new GetFilesResponse()
            {
                Files = client.Get<List<FileXTRF>>(request)
            };
        }

        [Action("Download file content", Description = "Download the content of a specific file")]
        public DownloadFileResponse DownloadFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string fileId, [ActionParameter] string fileName)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/files/" + fileId + "/download/" + fileName, Method.Get, authenticationCredentialsProviders);
            return new DownloadFileResponse()
            {
                FileContent = client.Execute(request).RawBytes
            };
        }

        [Action("Change project status", Description = "Change the status of a project")]
        public void ChangeProjectStatus(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string projectStatus)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/status", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new {
                status = projectStatus
            });
            client.Execute(request);
        }

        [Action("Upload a file to a project", Description = "Upload a file to a specific project")]
        public void UploadFileToProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] UploadFileToProjectRequest input)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var uploadRequest = new XtrfRequest("/v2/projects/" + input.ProjectId + "/files/upload", Method.Post, authenticationCredentialsProviders);
            uploadRequest.AddFile("file", input.File, input.FileName);
            var outputFileId = client.Post<UploadFileResponse>(uploadRequest).FileId;

            var addRequest = new XtrfRequest("/v2/projects/" + input.ProjectId + "/files/add", Method.Put, authenticationCredentialsProviders);
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

        [Action("Check for created or queued cat tool project", Description = "Check for created or queued cat tool project")]
        public CheckForCatToolResponse CheckForCatTool(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/catToolProject", Method.Get, authenticationCredentialsProviders);
            return client.Get<CheckForCatToolResponse>(request);
        }

        [Action("Get client contacts information for a project", Description = "Get client contacts information for a specific project")]
        public GetClientContactsByProjectResponse GetClientContactsByProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/clientContacts", Method.Get, authenticationCredentialsProviders);
            return client.Get<GetClientContactsByProjectResponse>(request);
        }

        [Action("Get finance information for a project", Description = "Get finance information for a specific project")]
        public FinanceInformation GetFinanceInfo(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/finance", Method.Get, authenticationCredentialsProviders);
            return client.Get<FinanceInformation>(request);
        }

        [Action("Get file details", Description = "Get details of a specific file")]
        public FileXTRF GetFileDetails(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string fileId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/files/" + fileId, Method.Get, authenticationCredentialsProviders);
            return client.Get<FileXTRF>(request);
        }

        [Action("Get process id for a project", Description = "Get process id for a specific project")]
        public GetProcessIdByProjectResponse GetProcessIdByProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/process", Method.Get, authenticationCredentialsProviders);
            return client.Get<GetProcessIdByProjectResponse>(request);
        }

        [Action("Delete a payable for a project", Description = "Delete a payable for a specific project")]
        public void DeletePayableForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] int payableId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/finance/payables/" + payableId, Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }

        [Action("Delete a receivable for a project", Description = "Delete a receivable for a specific project")]
        public void DeleteReceivableForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] int receivableId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/finance/receivables/" + receivableId, Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }

    }
}
