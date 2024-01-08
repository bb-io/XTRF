﻿using System.Net.Mime;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models;
using Apps.XTRF.Shared.Models.Entities;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Models.Entities;
using Apps.XTRF.Smart.Models.Identifiers;
using Apps.XTRF.Smart.Models.Requests.File;
using Apps.XTRF.Smart.Models.Requests.SmartProject;
using Apps.XTRF.Smart.Models.Responses.File;
using Apps.XTRF.Smart.Models.Responses.SmartJob;
using Apps.XTRF.Smart.Models.Responses.SmartProject;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Parsers;
using RestSharp;

namespace Apps.XTRF.Smart.Actions;

[ActionList]
public class SmartProjectActions : XtrfInvocable
{
    public SmartProjectActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #region Get

    [Action("Smart: Get project details", Description = "Get information about a smart project. If you need to retrieve " +
                                                        "client contacts, finance information, process ID or check if " +
                                                        "project created in CAT tool or creation is queued, set the " +
                                                        "respective optional parameter to 'True'")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] [Display("Include client contacts")] bool? includeClientContacts,
        [ActionParameter] [Display("Include finance information")] bool? includeFinanceInformation,
        [ActionParameter] [Display("Include process ID")] bool? includeProcessId,
        [ActionParameter] [Display("Include if project is created in CAT tool or queued")] bool? includeCatTool)
    {
        var getProjectRequest = new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<SmartProject>(getProjectRequest);
        var projectResponse = new ProjectResponse(project);

        if (includeClientContacts != null && includeClientContacts.Value)
        {
            var getContactsRequest = 
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/clientContacts", Method.Get, Creds);
            var contacts = await Client.ExecuteWithErrorHandling<SmartProjectContacts>(getContactsRequest);
            projectResponse.ProjectContacts = contacts;
        }
        
        if (includeFinanceInformation != null && includeFinanceInformation.Value)
        {
            var getFinanceInformationRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/finance", Method.Get, Creds);
            var financeInformation =
                await Client.ExecuteWithErrorHandling<FinanceInformation>(getFinanceInformationRequest);
            projectResponse.FinanceInformation = financeInformation;
        }
        
        if (includeProcessId != null && includeProcessId.Value)
        {
            var getProcessIdRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/process", Method.Get, Creds);
            var process = await Client.ExecuteWithErrorHandling<ProcessResponse>(getProcessIdRequest);
            projectResponse.ProcessId = process.ProcessId;
        }

        if (includeCatTool != null && includeCatTool.Value)
        {
            var checkForCatToolResponseRequest = 
                new XtrfRequest( $"/v2/projects/{projectIdentifier.ProjectId}/catToolProject", Method.Get, Creds);
            var checkForCatToolResponseResponse =
                await Client.ExecuteWithErrorHandling<CheckForCatToolResponse>(checkForCatToolResponseRequest);
            projectResponse.ProjectCreatedInCatToolOrCreationIsQueued =
                checkForCatToolResponseResponse.ProjectCreatedInCatToolOrCreationIsQueued;
        }

        return projectResponse;
    }

    [Action("Smart: List jobs in project", Description = "List all jobs in a smart project")]
    public async Task<ListJobsResponse> GetJobsInProject([ActionParameter] ProjectIdentifier projectIdentifier)
    {
        var request = new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/jobs", Method.Get, Creds);
        var jobs = await Client.ExecuteWithErrorHandling<IEnumerable<SmartJob>>(request);
        return new(jobs.Select(job => new JobResponse(job)));
    }
    
    [Action("Smart: List files in project", Description = "List all files in a smart project")]
    public async Task<ListFilesResponse> GetFilesByProject([ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] FilterLanguageOptionalIdentifier languageIdentifier,
        [ActionParameter] SmartFileCategoryOptionalIdentifier fileCategoryIdentifier)
    {
        var request = new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/files", Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<IEnumerable<SmartFileXTRF>>(request);
        var filteredFiles = files.Where(file =>
                languageIdentifier.LanguageId is null ||
                file.LanguageRelation.Languages.Contains(languageIdentifier.LanguageId))
            .Where(file =>
                fileCategoryIdentifier.Category is null || file.CategoryKey == fileCategoryIdentifier.Category);
    
        return new(filteredFiles);
    }
    
    [Action("Smart: Download file", Description = "Download the content of a specific file")]
    public async Task<FileWrapper> DownloadFile([ActionParameter] FileIdentifier fileIdentifier, 
        [ActionParameter] [Display("Filename")] string filename)
    {
        filename = filename.Trim();
        var request = new XtrfRequest($"/v2/projects/files/{fileIdentifier.FileId}/download/{filename}", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling(request);
    
        return new()
        {
            File = new(response.RawBytes)
            {
                Name = filename,
                ContentType = response.ContentType ?? MediaTypeNames.Application.Octet
            }
        };
    }
    
    [Action("Smart: Get project file details", Description = "Get information about specific file in a smart project")]
    public async Task<SmartFileXTRF> GetProjectFile([ActionParameter] FileIdentifier fileIdentifier)
    {
        var request = new XtrfRequest($"/v2/projects/files/{fileIdentifier.FileId}", Method.Get, Creds);
        var file = await Client.ExecuteWithErrorHandling<SmartFileXTRF>(request);
        return file;
    }

    #endregion

    #region Post

    [Action("Smart: Create new project", Description = "Create a new smart project")]
    public async Task<ProjectResponse> CreateProject([ActionParameter] CreateProjectRequest input)
    {
        var request = new XtrfRequest("/v2/projects", Method.Post, Creds)
            .WithJsonBody(new
            {
                name = input.Name,
                clientId = ConvertToInt64(input.ClientId, "Client"),
                serviceId = ConvertToInt64(input.ServiceId, "Service")
            }, JsonConfig.Settings);
    
        var project = await Client.ExecuteWithErrorHandling<SmartProject>(request);
        return new(project);
    }
    
    [Action("Smart: Upload file to project", Description = "Upload a file to a smart project")]
    public async Task<FileIdentifier> UploadFileToProject([ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] UploadFileRequest input)
    {
        var uploadFileRequest =
            new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/files/upload", Method.Post, Creds);
        uploadFileRequest.AddFile("file", input.File.Bytes, input.File.Name);
        var fileIdentifier = await Client.ExecuteWithErrorHandling<FileIdentifier>(uploadFileRequest);

        var addFileRequest = new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/files/add", Method.Put, Creds)
            .WithJsonBody(new
            {
                files = new[]
                {
                    new
                    {
                        category = input.Category,
                        fileId = fileIdentifier.FileId,
                        languageIds = input.LanguageId is not null
                            ? new[] { IntParser.Parse(input.LanguageId, nameof(input.LanguageId)) }
                            : null,
                        languageCombinationIds = input.SourceLanguageId is not null
                                                 && input.TargetLanguageId is not null
                            ? new[]
                            {
                                new
                                {
                                    sourceLanguageId =
                                        IntParser.Parse(input.SourceLanguageId, nameof(input.SourceLanguageId)),
                                    targetLanguageId =
                                        IntParser.Parse(input.TargetLanguageId, nameof(input.TargetLanguageId))
                                }
                            }
                            : null
                    }
                }
            }, JsonConfig.Settings);

        await Client.ExecuteWithErrorHandling(addFileRequest);
        return fileIdentifier;
    }

    #endregion

    #region Put

    [Action("Smart: Add target languages to project", Description = "Add more target languages to a smart project")]
    public async Task<ProjectIdentifier> AddTargetLanguageToProject(
        [ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] [Display("Target languages")] [DataSource(typeof(LanguageDataHandler))] 
        IEnumerable<string> targetLanguageIds)
    {
        var getProjectRequest = new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<SmartProject>(getProjectRequest);
        var targetLanguages = project.Languages.TargetLanguageIds.Union(targetLanguageIds);
        
        var updateTargetLanguagesRequest =
            new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/targetLanguages", Method.Put, Creds)
                .WithJsonBody(new
                {
                    targetLanguageIds = ConvertToInt64Enumerable(targetLanguages, "Target languages")
                });
        
        await Client.ExecuteWithErrorHandling(updateTargetLanguagesRequest);
        return projectIdentifier;
    }
    
    [Action("Smart: Update project", Description = "Update a smart project, specifying only the fields that require updating")]
    public async Task<ProjectIdentifier> UpdateProject([ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] UpdateProjectRequest input)
    {
        if (input.Status != null)
        {
            var updateStatusRequest = 
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/status", Method.Put, Creds)
                    .WithJsonBody(new { status = input.Status });
            
            await Client.ExecuteWithErrorHandling(updateStatusRequest);
        }

        if (input.SourceLanguageId != null)
        {
            var updateSourceLanguageRequest = 
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/sourceLanguage", Method.Put, Creds)
                    .WithJsonBody(new { sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language") });

            await Client.ExecuteWithErrorHandling(updateSourceLanguageRequest);
        }

        if (input.TargetLanguageIds != null)
        {
            var updateTargetLanguagesRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/targetLanguages", Method.Put, Creds)
                    .WithJsonBody(new
                    {
                        targetLanguageIds = ConvertToInt64Enumerable(input.TargetLanguageIds, "Target languages")
                    });

            await Client.ExecuteWithErrorHandling(updateTargetLanguagesRequest);
        }

        if (input.SpecializationId != null)
        {
            var updateSpecializationRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/specialization", Method.Put, Creds)
                    .WithJsonBody(new { specializationId = ConvertToInt64(input.SpecializationId, "Specialization") });
            
            await Client.ExecuteWithErrorHandling(updateSpecializationRequest);
        }

        if (input.PrimaryId != null || input.AdditionalIds != null)
        {
            object requestBody;

            if (input.PrimaryId == null || input.AdditionalIds == null)
            {
                var getContactsRequest =
                    new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/clientContacts", Method.Get, Creds);
                var contacts = await Client.ExecuteWithErrorHandling<SmartProjectContacts>(getContactsRequest);
                requestBody = new
                {
                    primaryId = ConvertToInt64(input.PrimaryId ?? contacts.PrimaryId, "Primary contact person"),
                    additionalIds = ConvertToInt64Enumerable(input.AdditionalIds ?? contacts.AdditionalIds,
                        "Additional contact persons")
                };
            }
            else
                requestBody = new
                {
                    primaryId = ConvertToInt64(input.PrimaryId, "Primary contact person"),
                    additionalIds = ConvertToInt64Enumerable(input.AdditionalIds, "Additional contact persons")
                };

            var updateContactsRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/clientContacts", Method.Put, Creds)
                    .WithJsonBody(requestBody);
            
            await Client.ExecuteWithErrorHandling(updateContactsRequest);
        }

        if (input.ClientDeadline != null)
        {
            var updateClientDeadlineRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/clientDeadline", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientDeadline?.ConvertToUnixTime() });

            await Client.ExecuteWithErrorHandling(updateClientDeadlineRequest);
        }

        if (input.OrderDate != null)
        {
            var updateClientDeadlineRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/orderDate", Method.Put, Creds)
                    .WithJsonBody(new { value = input.OrderDate?.ConvertToUnixTime() });

            await Client.ExecuteWithErrorHandling(updateClientDeadlineRequest);
        }

        if (input.ClientNotes != null)
        {
            var updateClientNotesRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/clientNotes", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientNotes });

            await Client.ExecuteWithErrorHandling(updateClientNotesRequest);
        }

        if (input.InternalNotes != null)
        {
            var updateInternalNotesRequest = 
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/internalNotes", Method.Put, Creds)
                    .WithJsonBody(new { value = input.InternalNotes });

            await Client.ExecuteWithErrorHandling(updateInternalNotesRequest);
        }

        if (input.VendorInstructions != null)
        {
            var updateVendorInstructionsRequest = 
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/vendorInstructions", Method.Put, Creds)
                    .WithJsonBody(new { value = input.VendorInstructions });

            await Client.ExecuteWithErrorHandling(updateVendorInstructionsRequest);
        }

        if (input.ClientReferenceNumber != null)
        {
            var updateClientReferenceNumberRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/clientReferenceNumber", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientReferenceNumber });

            await Client.ExecuteWithErrorHandling(updateClientReferenceNumberRequest);
        }

        if (input.Volume != null)
        {
            var updateVolumeRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/volume", Method.Put, Creds)
                    .WithJsonBody(new { value = input.Volume });

            await Client.ExecuteWithErrorHandling(updateVolumeRequest);
        }

        return projectIdentifier;
    }

    #endregion

    #region Delete

    [Action("Smart: Delete payable for project", Description = "Delete a payable for a smart project")]
    public async Task DeletePayableForProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartPayableIdentifier payableIdentifier)
    {
        var request =
            new XtrfRequest(
                $"/v2/projects/{projectIdentifier.ProjectId}/finance/payables/{payableIdentifier.PayableId}",
                Method.Delete, Creds);
        
        await Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Smart: Delete receivable for project", Description = "Delete a receivable for a smart project")]
    public async Task DeleteReceivableForProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] SmartReceivableIdentifier receivableIdentifier)
    {
        var request =
            new XtrfRequest(
                $"/v2/projects/{projectIdentifier.ProjectId}/finance/receivables/{receivableIdentifier.ReceivableId}",
                Method.Delete, Creds);
        
        await Client.ExecuteWithErrorHandling(request);
    }

    #endregion
}