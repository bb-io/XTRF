using System.Net.Mime;
using Apps.XTRF.Api;
using Apps.XTRF.Constants;
using Apps.XTRF.Extensions;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.InputParameters;
using Apps.XTRF.Models.Requests;
using Apps.XTRF.Models.Requests.Project;
using Apps.XTRF.Models.Responses;
using Apps.XTRF.Models.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
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
    public async Task<ProjectResponse> GetProject([ActionParameter] [Display("Project ID")] string projectId)
    {
        var request = new XtrfRequest("/v2/projects/" + projectId, Method.Get, Creds);
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
    public async Task<GetJobsResponse> GetJobsByProject([ActionParameter] [Display("Project ID")] string projectId)
    {
        var endpoint = "/v2/projects/" + projectId + "/jobs";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        var responseJobs = await Client.ExecuteWithErrorHandling<List<JobResponse>>(request);
        var dtoJobs = new List<JobDTO>();

        foreach (var job in responseJobs)
            dtoJobs.Add(new(job));

        return new()
        {
            Jobs = dtoJobs
        };
    }

    [Action("Get files in a project", Description = "Get all files of a specific project")]
    public async Task<GetFilesResponse> GetFilesByProject([ActionParameter] GetFilesInProjectRequest input)
    {
        var endpoint = "/v2/projects/" + input.ProjectId + "/files";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        var files = await Client.ExecuteWithErrorHandling<List<FileXTRF>>(request);

        return new()
        {
            Files = files.Where(x => input.LanguageId is null || x.Languages.Contains(int.Parse(input.LanguageId)))
                .Where(x => input.Category is null || x.CategoryKey == input.Category)
        };
    }

    [Action("Download file content", Description = "Download the content of a specific file")]
    public async Task<DownloadFileResponse> DownloadFile(
        [ActionParameter] [Display("File ID")] string fileId,
        [ActionParameter] [Display("File name")]
        string fileName)
    {
        var endpoint = "/v2/projects/files/" + fileId + "/download/" + fileName;
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
    public Task ChangeProjectStatus([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Project status")]
        string projectStatus)
    {
        var endpoint = "/v2/projects/" + projectId + "/status";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            status = projectStatus
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Upload a file to a project", Description = "Upload a file to a specific project")]
    public async Task UploadFileToProject([ActionParameter] UploadFileToProjectRequest input)
    {
        var uploadEndpoint = "/v2/projects/" + input.ProjectId + "/files/upload";
        var uploadRequest = new XtrfRequest(uploadEndpoint, Method.Post, Creds);
        uploadRequest.AddFile("file", input.File.Bytes, input.FileName ?? input.File.Name);

        var outputFileId = (await Client.ExecuteWithErrorHandling<UploadFileResponse>(uploadRequest)).FileId;

        var addEndpoint = "/v2/projects/" + input.ProjectId + "/files/add";
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
    public Task<CheckForCatToolResponse> CheckForCatTool([ActionParameter] [Display("Project ID")] string projectId)
    {
        var endpoint = "/v2/projects/" + projectId + "/catToolProject";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<CheckForCatToolResponse>(request)!;
    }

    [Action("Get client contacts information for a project",
        Description = "Get client contacts information for a specific project")]
    public Task<GetClientContactsByProjectResponse> GetClientContactsByProject(
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var endpoint = "/v2/projects/" + projectId + "/clientContacts";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<GetClientContactsByProjectResponse>(request);
    }

    [Action("Get finance information for a project", Description = "Get finance information for a specific project")]
    public Task<FinanceInformation> GetFinanceInfo([ActionParameter] [Display("Project ID")] string projectId)
    {
        var endpoint = "/v2/projects/" + projectId + "/finance";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<FinanceInformation>(request);
    }

    [Action("Get project file details", Description = "Get details of a specific file in a project")]
    public Task<FileXTRF> GetProjectFileDetails([ActionParameter] [Display("File ID")] string fileId)
    {
        var endpoint = "/v2/projects/files/" + fileId;
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<FileXTRF>(request);
    }

    [Action("Get process ID for a project", Description = "Get process ID for a specific project")]
    public Task<GetProcessIdByProjectResponse> GetProcessIdByProject(
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var endpoint = "/v2/projects/" + projectId + "/process";
        var request = new XtrfRequest(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<GetProcessIdByProjectResponse>(request);
    }

    [Action("Delete a payable for a project", Description = "Delete a payable for a specific project")]
    public Task DeletePayableForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Payable ID")]
        int payableId)
    {
        var endpoint = "/v2/projects/" + projectId + "/finance/payables/" + payableId;
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Delete a receivable for a project", Description = "Delete a receivable for a specific project")]
    public Task DeleteReceivableForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Receivable ID")]
        int receivableId)
    {
        var endpoint = "/v2/projects/" + projectId + "/finance/receivables/" + receivableId;
        var request = new XtrfRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update client deadline for a project", Description = "Update client deadline for a specific project")]
    public Task UpdateDeadlineForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Deadline date")]
        string deadlineDate)
    {
        var endpoint = "/v2/projects/" + projectId + "/clientDeadline";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = deadlineDate.ConvertToUnixTime()
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update client notes for a project", Description = "Update client notes for a specific project")]
    public Task UpdateClientNotesForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Client notes")]
        string clientNotes)
    {
        var endpoint = "/v2/projects/" + projectId + "/clientNotes";
        var request = new XtrfRequest(endpoint, Method.Put,
            Creds);
        request.AddJsonBody(new
        {
            value = clientNotes
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update internal notes for a project", Description = "Update internal notes for a specific project")]
    public Task UpdateInternalNotesForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Internal notes")]
        string internalNotes)
    {
        var endpoint = "/v2/projects/" + projectId + "/internalNotes";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = internalNotes
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update client reference number for a project",
        Description = "Update client reference number for a specific project")]
    public Task UpdateClientReferenceNumberForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Reference number")]
        string referenceNumber)
    {
        var endpoint = "/v2/projects/" + projectId + "/clientReferenceNumber";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = referenceNumber
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update order date for a project", Description = "Update order date for a specific project")]
    public Task UpdateOrderDateForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Order date")]
        string orderDate)
    {
        var endpoint = "/v2/projects/" + projectId + "/orderDate";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = orderDate.ConvertToUnixTime()
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update source language for a project", Description = "Update source language for a specific project")]
    public Task UpdateSourceLanguageForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Source language ID")]
        string sourceLanguageId)
    {
        if (!int.TryParse(sourceLanguageId, out var intLangId))
            throw new("Source language ID must be a number");

        var endpoint = "/v2/projects/" + projectId + "/sourceLanguage";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            sourceLanguageId = intLangId
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
    public async Task AddTargetLanguageToProject([ActionParameter] AddTargetLanguagesToProjectRequest input)
    {
        var project = await GetProject(input.ProjectId);

        var projectTargLangs = project.TargetLanguageIds ?? Enumerable.Empty<string>();
        var request = new UpdateProjectTargetLanguagesRequest()
        {
            ProjectId = input.ProjectId,
            TargetLanguageIds = projectTargLangs.Append(input.TargetLanguageId)
        };

        await UpdateTargetLanguagesForProject(request);
    }

    [Action("Update specialization for a project", Description = "Update specialization for a specific project")]
    public Task UpdateSpecializationForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Specialization ID")]
        string specializationId)
    {
        if (!int.TryParse(specializationId, out var intSpecializationId))
            throw new("Specialization ID must be a number");

        var endpoint = "/v2/projects/" + projectId + "/specialization";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            specializationId = intSpecializationId
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update vendor instructions for a project",
        Description = "Update vendor instructions for a specific project")]
    public Task UpdateVendorInstructionsForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Vendor instructions")]
        string vendorInstructions)
    {
        var endpoint = "/v2/projects/" + projectId + "/vendorInstructions";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = vendorInstructions
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Update volume for a project", Description = "Update volume for a specific project")]
    public Task UpdateVolumeForProject([ActionParameter] [Display("Project ID")] string projectId,
        [ActionParameter] [Display("Volume")] int volume)
    {
        var endpoint = "/v2/projects/" + projectId + "/volume";
        var request = new XtrfRequest(endpoint, Method.Put, Creds);
        request.AddJsonBody(new
        {
            value = volume
        });

        return Client.ExecuteWithErrorHandling(request);
    }
}