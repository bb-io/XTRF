﻿using Apps.XTRF.Requests;
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
        public Project GetProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId, Method.Get, authenticationCredentialsProviders);
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

        [Action("Get project file details", Description = "Get details of a specific file in a project")]
        public FileXTRF GetProjectFileDetails(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string fileId)
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

        [Action("Update client deadline for a project", Description = "Update client deadline for a specific project")]
        public void UpdateDeadlineForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string deadlineDate)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/clientDeadline", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = ConvertStringToUnixTime(deadlineDate)
            });
            client.Execute(request);
        }

        [Action("Update client notes for a project", Description = "Update client notes for a specific project")]
        public void UpdateClientNotesForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string clientNotes)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/clientNotes", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = clientNotes
            });
            client.Execute(request);
        }

        [Action("Update internal notes for a project", Description = "Update internal notes for a specific project")]
        public void UpdateInternalNotesForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string internalNotes)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/internalNotes", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = internalNotes
            });
            client.Execute(request);
        }

        [Action("Update client reference number for a project", Description = "Update client reference number for a specific project")]
        public void UpdateClientReferenceNumberForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string referenceNumber)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/clientReferenceNumber", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = referenceNumber
            });
            client.Execute(request);
        }

        [Action("Update order date for a project", Description = "Update order date for a specific project")]
        public void UpdateOrderDateForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string orderDate)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/orderDate", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = ConvertStringToUnixTime(orderDate)
            });
            client.Execute(request);
        }

        [Action("Update source language for a project", Description = "Update source language for a specific project")]
        public void UpdateSourceLanguageForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] int sourceLanguageId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/sourceLanguage", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                sourceLanguageId = sourceLanguageId
            });
            client.Execute(request);
        }

        [Action("Update specialization for a project", Description = "Update specialization for a specific project")]
        public void UpdateSpecializationForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] int specializationId)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/specialization", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                specializationId = specializationId
            });
            client.Execute(request);
        }

        [Action("Update vendor instructions for a project", Description = "Update vendor instructions for a specific project")]
        public void UpdateVendorInstructionsForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] string vendorInstructions)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/vendorInstructions", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = vendorInstructions
            });
            client.Execute(request);
        }

        [Action("Update volume for a project", Description = "Update volume for a specific project")]
        public void UpdateVolumeForProject(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, [ActionParameter] string projectId, [ActionParameter] int volume)
        {
            var client = new XtrfClient(authenticationCredentialsProviders);
            var request = new XtrfRequest("/v2/projects/" + projectId + "/volume", Method.Put, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                value = volume
            });
            client.Execute(request);
        }

        public long ConvertStringToUnixTime(string inputDate)
        {
            DateTime date = DateTime.Parse(inputDate).ToUniversalTime();
            var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);

            DateTimeOffset offset = new DateTimeOffset(unspecifiedDateKind);
            long unixTime = offset.ToUnixTimeMilliseconds();

            return unixTime;
        }

    }
}
