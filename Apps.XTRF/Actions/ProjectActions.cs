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

        //[Action("Download file content", Description = "Download the content of a specific file")]
        //public byte[] DownloadFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string fileId, [ActionParameter] string fileName)
        //{
        //    var client = new XtrfClient(authenticationCredentialsProviders);
        //    var request = new XtrfRequest("/v2/projects/files/" + fileId + "/download/" + fileName, Method.Get, authenticationCredentialsProviders);
        //    return client.Execute(request).RawBytes;
            
        //}

        //[Action("Change project status", Description = "Change the status of a project")]
        //public void ChangeProjectStatus(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string status)
        //{
        //    var client = new XtrfClient(authenticationCredentialsProviders);
        //    var request = new XtrfRequest("/v2/projects/" + projectId + "/status", Method.Put, authenticationCredentialsProviders);
        //    request.AddJsonBody(status);
        //    client.Put(request);
        //}

        //[Action("Uplaod a file to a project", Description = "Upload a file to a specific project")]
        //public string UploadFileToProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] UploadFileRequest input)
        //{
        //    var client = new XtrfClient(authenticationCredentialsProviders);
        //    var request = new XtrfRequest("/v2/projects/" + input.ProjectId + "/files/upload", Method.Post, authenticationCredentialsProviders);
        //    request.AddHeader("Content-Disposition", $"filename*=UTF-8''{input.FileName}");
        //    request.AddHeader("Content-Type", "application/octet-stream");
        //    var text = "This is the content of a file";
        //    byte[] fileContent = Encoding.ASCII.GetBytes(text);
        //    request.AddParameter("application/octet-stream", fileContent, ParameterType.RequestBody);
        //    var response = client.Post<UploadFileResponse>(request);
        //    return response.FileId;
        //}
    }
}
