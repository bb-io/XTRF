using System.Net.Mime;
using Apps.XTRF.Api;
using Apps.XTRF.Constants;
using Apps.XTRF.DataSourceHandlers;
using Apps.XTRF.Extensions;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Identifiers;
using Apps.XTRF.Models.Requests;
using Apps.XTRF.Models.Requests.CustomField;
using Apps.XTRF.Models.Requests.Project;
using Apps.XTRF.Models.Responses;
using Apps.XTRF.Models.Responses.CustomField;
using Apps.XTRF.Models.Responses.Entities;
using Apps.XTRF.Models.Responses.File;
using Apps.XTRF.Models.Responses.Job;
using Apps.XTRF.Models.Responses.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Parsers;
using RestSharp;

namespace Apps.XTRF.Actions;

[ActionList]
public class ProjectActions : XtrfInvocable
{
    public ProjectActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get project details", Description = "Get all information of a specific project")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectIdentifier project)
    {
        var request = new XtrfRequest($"/v2/projects/{project.ProjectId}", Method.Get, Creds);
        return new(await Client.ExecuteWithErrorHandling<Project>(request));
    }

    [Action("Create new project", Description = "Create a new project")]
    public async Task<ProjectResponse> CreateProject([ActionParameter] CreateProjectInput project)
    {
        var request = new XtrfRequest("/v2/projects", Method.Post, Creds)
            .WithJsonBody(new CreateProjectRequest(project), JsonConfig.Settings);
    
        return new(await Client.ExecuteWithErrorHandling<Project>(request));
    }
    
    [Action("Get jobs in a project", Description = "Get all jobs of a specific project")]
    public async Task<GetJobsResponse> GetJobsByProject([ActionParameter] ProjectIdentifier project)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/jobs";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        var jobs = await Client.ExecuteWithErrorHandling<List<Job>>(request);
        return new() { Jobs = jobs };
    }
    
    [Action("Get files in a project", Description = "Get all files of a specific project")]
    public async Task<GetFilesResponse> GetFilesByProject([ActionParameter] GetFilesInProjectRequest input)
    {
        var endpoint = $"/v2/projects/{input.ProjectId}/files";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        var files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request);
    
        return new()
        {
            Files = files.Where(file => input.LanguageId is null || file.LanguageRelation.Languages.Contains(input.LanguageId))
                .Where(file => input.Category is null || file.CategoryKey == input.Category)
        };
    }
    
    [Action("Download file content", Description = "Download the content of a specific file")]
    public async Task<DownloadFileResponse> DownloadFile([ActionParameter] FileIdentifier file, 
        [ActionParameter] [Display("File name")] string fileName)
    {
        var endpoint = $"/v2/projects/files/{file.FileId}/download/{fileName}";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling(request);
    
        return new()
        {
            File = new(response.RawBytes)
            {
                Name = fileName,
                ContentType = response.ContentType ?? MediaTypeNames.Application.Octet
            }
        };
    }
    
    [Action("Change project status", Description = "Change the status of a project")]
    public Task ChangeProjectStatus([ActionParameter] ProjectIdentifier project, 
        [ActionParameter] [Display("Project status")] string projectStatus)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/status";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            status = projectStatus
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Upload a file to a project", Description = "Upload a file to a specific project")]
    public async Task UploadFileToProject([ActionParameter] ProjectIdentifier project, 
        [ActionParameter] UploadFileToProjectRequest input)
    {
        var uploadEndpoint = $"/v2/projects/{project.ProjectId}/files/upload";
        var uploadRequest = new XtrfRequest(uploadEndpoint, Method.Post, Creds);
        uploadRequest.AddFile("file", input.File.Bytes, input.FileName ?? input.File.Name);
    
        var outputFileId = (await Client.ExecuteWithErrorHandling<UploadFileResponse>(uploadRequest)).FileId;
    
        var addEndpoint = $"/v2/projects/{project.ProjectId}/files/add";
        var addRequest = new XtrfRequest(addEndpoint, Method.Put, Creds);
        addRequest.AddJsonBody(new
        {
            files = new[]
            {
                new
                {
                    category = input.Category,
                    fileId = outputFileId,
                    languageIds = input.LanguageId is not null
                        ? new[] { IntParser.Parse(input.LanguageId, nameof(input.LanguageId))!.Value }
                        : null,
                    languageCombinationIds = input.SourceLanguageId is not null
                                             && input.TargetLanguageId is not null
                        ? new[]
                        {
                            new
                            {
                                sourceLanguageId =
                                    IntParser.Parse(input.SourceLanguageId, nameof(input.SourceLanguageId)),
                                targetLanguageId = IntParser.Parse(input.TargetLanguageId,
                                    nameof(input.TargetLanguageId))
                            }
                        }
                        : null
                }
            }
        });
    
        await Client.ExecuteWithErrorHandling(addRequest);
    }
    
    [Action("Check for created or queued cat tool project",
        Description = "Check for created or queued cat tool project")]
    public Task<CheckForCatToolResponse> CheckForCatTool([ActionParameter] ProjectIdentifier project)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/catToolProject";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return Client.ExecuteWithErrorHandling<CheckForCatToolResponse>(request)!;
    }
    
    [Action("Get client contacts information for a project",
        Description = "Get client contacts information for a specific project")]
    public Task<GetClientContactsByProjectResponse> GetClientContactsByProject(
        [ActionParameter] ProjectIdentifier project)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/clientContacts";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return Client.ExecuteWithErrorHandling<GetClientContactsByProjectResponse>(request);
    }
    
    [Action("Get finance information for a project", Description = "Get finance information for a specific project")]
    public Task<FinanceInformation> GetFinanceInfo([ActionParameter] ProjectIdentifier project)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/finance";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return Client.ExecuteWithErrorHandling<FinanceInformation>(request);
    }
    
    [Action("Get project file details", Description = "Get details of a specific file in a project")]
    public Task<FileXTRF> GetProjectFileDetails([ActionParameter] FileIdentifier file)
    {
        var endpoint = "/v2/projects/files/" + file.FileId;
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return Client.ExecuteWithErrorHandling<FileXTRF>(request);
    }
    
    [Action("Get process ID for a project", Description = "Get process ID for a specific project")]
    public Task<GetProcessIdByProjectResponse> GetProcessIdByProject([ActionParameter] ProjectIdentifier project)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/process";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
        return Client.ExecuteWithErrorHandling<GetProcessIdByProjectResponse>(request);
    }
    
    [Action("Delete a payable for a project", Description = "Delete a payable for a specific project")]
    public Task DeletePayableForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Payable ID")] int payableId)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/finance/payables/" + payableId;
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Delete a receivable for a project", Description = "Delete a receivable for a specific project")]
    public Task DeleteReceivableForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Receivable ID")] int receivableId)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/finance/receivables/" + receivableId;
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update client deadline for a project", Description = "Update client deadline for a specific project")]
    public Task UpdateDeadlineForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Deadline")] DateTime deadline)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/clientDeadline";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = deadline.ConvertToUnixTime()
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update client notes for a project", Description = "Update client notes for a specific project")]
    public Task UpdateClientNotesForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Client notes")] string clientNotes)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/clientNotes";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = clientNotes
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update internal notes for a project", Description = "Update internal notes for a specific project")]
    public Task UpdateInternalNotesForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Internal notes")] string internalNotes)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/internalNotes";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = internalNotes
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update client reference number for a project",
        Description = "Update client reference number for a specific project")]
    public Task UpdateClientReferenceNumberForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Reference number")] string referenceNumber)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/clientReferenceNumber";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = referenceNumber
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update order date for a project", Description = "Update order date for a specific project")]
    public Task UpdateOrderDateForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Order date")] DateTime orderDate)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/orderDate";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = orderDate.ConvertToUnixTime()
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update source language for a project", Description = "Update source language for a specific project")]
    public Task UpdateSourceLanguageForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Source language ID")] [DataSource(typeof(LanguageDataHandler))] string sourceLanguageId)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/sourceLanguage";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            sourceLanguageId = long.Parse(sourceLanguageId)
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update target languages for a project", Description = "Update target languages for a specific project")]
    public Task UpdateTargetLanguagesForProject([ActionParameter] UpdateProjectTargetLanguagesRequest input)
    {
        var endpoint = "/v2/projects/" + input.ProjectId + "/targetLanguages";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            targetLanguageIds = input.TargetLanguageIds.Select(x => IntParser.Parse(x, "targetLanguageId"))
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Add target language to project", Description = "Add one more target language to a specific project")]
    public async Task AddTargetLanguageToProject([ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] AddTargetLanguagesToProjectRequest input)
    {
        var project = await GetProject(projectIdentifier);
    
        var projectTargLangs = project.TargetLanguageIds ?? Enumerable.Empty<string>();
        var request = new UpdateProjectTargetLanguagesRequest
        {
            ProjectId = projectIdentifier.ProjectId,
            TargetLanguageIds = projectTargLangs.Append(input.TargetLanguageId)
        };
    
        await UpdateTargetLanguagesForProject(request);
    }
    
    [Action("Update specialization for a project", Description = "Update specialization for a specific project")]
    public Task UpdateSpecializationForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Specialization ID")] string specializationId)
    {
        if (!int.TryParse(specializationId, out var intSpecializationId))
            throw new("Specialization ID must be a number");
    
        var endpoint = "/v2/projects/" + project.ProjectId + "/specialization";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            specializationId = intSpecializationId
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update vendor instructions for a project",
        Description = "Update vendor instructions for a specific project")]
    public Task UpdateVendorInstructionsForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Vendor instructions")] string vendorInstructions)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/vendorInstructions";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = vendorInstructions
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("Update volume for a project", Description = "Update volume for a specific project")]
    public Task UpdateVolumeForProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] [Display("Volume")] int volume)
    {
        var endpoint = "/v2/projects/" + project.ProjectId + "/volume";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = volume
        });
    
        return Client.ExecuteWithErrorHandling(request);
    }
    
    [Action("List project custom fields", Description = "List custom fields of a specific project")]
    public async Task<ListCustomFieldsResponse> ListProjectCustomFields(
        [ActionParameter] ProjectIdentifier project)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/customFields";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);
    
        var response = await Client.ExecuteWithErrorHandling<CustomFieldEntity[]>(request);
        return new(response);
    }
    
    [Action("Update project custom field", Description = "Update custom field of a specific project")]
    public Task UpdateProjectCustomField([ActionParameter] ProjectIdentifier project,
        [ActionParameter] UpdateCustomFieldInput input)
    {
        var endpoint = $"/v2/projects/{project.ProjectId}/customFields/{input.Key}";
        var request = new XtrfRequest(endpoint, Method.Put, Creds)
            .WithJsonBody(new UpdateCustomFieldRequest(input.Value), JsonConfig.Settings);
    
        return Client.ExecuteWithErrorHandling(request);
    }
}