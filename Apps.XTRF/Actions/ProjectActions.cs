﻿using Apps.XTRF.Requests;
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

        //[Action("Uplaod a file to a project", Description = "Upload a file to a specific project")]
        //public void UploadFileToProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] UploadFileRequest input)
        //{
        //    var client = new XtrfClient(authenticationCredentialsProviders);
        //    var uploadRequest = new XtrfRequest("/v2/projects/" + input.ProjectId + "/files/upload", Method.Post, authenticationCredentialsProviders);
        //    var text = "This is the content of a file";
        //    byte[] fileContent = Encoding.ASCII.GetBytes(text);
        //    uploadRequest.AddFile("file", fileContent, input.FileName);
        //    var outputFileId = client.Execute(uploadRequest).Content;

        //    var addRequest = new XtrfRequest("/v2/projects/" + input.ProjectId + "/files/add", Method.Put, authenticationCredentialsProviders);
        //    addRequest.AddJsonBody(new
        //    {
        //        files = new[] 
        //        { 
        //            new 
        //            {
        //                category = input.Category,
        //                fileId = outputFileId
        //            }
        //        }
        //    });

        //    client.Execute(addRequest);
        //}
    }
}
