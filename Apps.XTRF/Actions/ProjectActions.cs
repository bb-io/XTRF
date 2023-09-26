using System.Net.Mime;
using Apps.XTRF.InputParameters;
using Apps.XTRF.Requests;
using Apps.XTRF.Requests.Project;
using Apps.XTRF.Responses;
using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF.Actions;

[ActionList]
public class ProjectActions
{
    [Action("Get project details", Description = "Get all information of a specific project")]
    public async Task<ProjectResponse> GetProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId, Method.Get, authenticationCredentialsProviders);

        return new(await client.ExecuteRequestAsync<Project>(request));
    }

    [Action("Create new project", Description = "Create a new project")]
    public async Task<ProjectResponse> CreateProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateProjectInput project)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects", Method.Post, authenticationCredentialsProviders);
        request.AddJsonBody(new CreateProjectRequest(project));
        return new(await client.ExecuteRequestAsync<Project>(request));
    }

    [Action("Get jobs in a project", Description = "Get all jobs of a specific project")]
    public GetJobsResponse GetJobsByProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/jobs", Method.Get,
            authenticationCredentialsProviders);
        var responseJobs = client.ExecuteRequest<List<JobResponse>>(request);
        var dtoJobs = new List<JobDTO>();

        foreach (var job in responseJobs)
            dtoJobs.Add(ExtensionMethods.MapJobResponseToDTO(job));

        return new()
        {
            Jobs = dtoJobs
        };
    }

    [Action("Get files in a project", Description = "Get all files of a specific project")]
    public GetFilesResponse GetFilesByProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/files", Method.Get,
            authenticationCredentialsProviders);
        return new GetFilesResponse()
        {
            Files = client.ExecuteRequest<List<FileXTRF>>(request)
        };
    }

    [Action("Download file content", Description = "Download the content of a specific file")]
    public DownloadFileResponse DownloadFile(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("File ID")] string fileId,
        [ActionParameter] [Display("File name")]
        string fileName)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/files/" + fileId + "/download/" + fileName, Method.Get,
            authenticationCredentialsProviders);

        var response = client.ExecuteRequest(request);
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
    public void ChangeProjectStatus(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Project status")]
        string projectStatus)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/status", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            status = projectStatus
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Upload a file to a project", Description = "Upload a file to a specific project")]
    public void UploadFileToProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UploadFileToProjectRequest input)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var uploadRequest = new XtrfRequest("/v2/projects/" + input.ProjectId + "/files/upload", Method.Post,
            authenticationCredentialsProviders);
        uploadRequest.AddFile("file", input.File.Bytes, input.FileName ?? input.File.Name);
        var outputFileId = client.Post<UploadFileResponse>(uploadRequest).FileId;

        var addRequest = new XtrfRequest("/v2/projects/" + input.ProjectId + "/files/add", Method.Put,
            authenticationCredentialsProviders);
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

    [Action("Check for created or queued cat tool project",
        Description = "Check for created or queued cat tool project")]
    public CheckForCatToolResponse CheckForCatTool(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/catToolProject", Method.Get,
            authenticationCredentialsProviders);
        return client.Get<CheckForCatToolResponse>(request);
    }

    [Action("Get client contacts information for a project",
        Description = "Get client contacts information for a specific project")]
    public GetClientContactsByProjectResponse GetClientContactsByProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/clientContacts", Method.Get,
            authenticationCredentialsProviders);
        return client.ExecuteRequest<GetClientContactsByProjectResponse>(request);
    }

    [Action("Get finance information for a project", Description = "Get finance information for a specific project")]
    public FinanceInformation GetFinanceInfo(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/finance", Method.Get,
            authenticationCredentialsProviders);
        return client.ExecuteRequest<FinanceInformation>(request);
    }

    [Action("Get project file details", Description = "Get details of a specific file in a project")]
    public FileXTRF GetProjectFileDetails(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("File ID")] string fileId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/files/" + fileId, Method.Get, authenticationCredentialsProviders);
        return client.ExecuteRequest<FileXTRF>(request);
    }

    [Action("Get process ID for a project", Description = "Get process ID for a specific project")]
    public GetProcessIdByProjectResponse GetProcessIdByProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/process", Method.Get,
            authenticationCredentialsProviders);
        return client.ExecuteRequest<GetProcessIdByProjectResponse>(request);
    }

    [Action("Delete a payable for a project", Description = "Delete a payable for a specific project")]
    public void DeletePayableForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Payable ID")]
        int payableId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/finance/payables/" + payableId, Method.Delete,
            authenticationCredentialsProviders);
        client.ExecuteRequest<object>(request);
    }

    [Action("Delete a receivable for a project", Description = "Delete a receivable for a specific project")]
    public void DeleteReceivableForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Receivable ID")]
        int receivableId)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/finance/receivables/" + receivableId,
            Method.Delete, authenticationCredentialsProviders);
        client.ExecuteRequest<object>(request);
    }

    [Action("Update client deadline for a project", Description = "Update client deadline for a specific project")]
    public void UpdateDeadlineForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Deadline date")]
        string deadlineDate)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/clientDeadline", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            value = deadlineDate.ConvertToUnixTime()
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Update client notes for a project", Description = "Update client notes for a specific project")]
    public void UpdateClientNotesForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Client notes")]
        string clientNotes)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/clientNotes", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            value = clientNotes
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Update internal notes for a project", Description = "Update internal notes for a specific project")]
    public void UpdateInternalNotesForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Internal notes")]
        string internalNotes)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/internalNotes", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            value = internalNotes
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Update client reference number for a project",
        Description = "Update client reference number for a specific project")]
    public void UpdateClientReferenceNumberForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Reference number")]
        string referenceNumber)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/clientReferenceNumber", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            value = referenceNumber
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Update order date for a project", Description = "Update order date for a specific project")]
    public void UpdateOrderDateForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Order date")]
        string orderDate)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/orderDate", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            value = orderDate.ConvertToUnixTime()
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Update source language for a project", Description = "Update source language for a specific project")]
    public void UpdateSourceLanguageForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Source language ID")]
        string sourceLanguageId)
    {
        if (!int.TryParse(sourceLanguageId, out var intLangId))
            throw new("Source language ID must be a number");

        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/sourceLanguage", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            sourceLanguageId = intLangId
        });
        client.Execute(request);
    }

    [Action("Update target languages for a project", Description = "Update target languages for a specific project")]
    public Task UpdateTargetLanguagesForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UpdateProjectTargetLanguagesRequest input)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + input.ProjectId + "/targetLanguages", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            targetLanguageIds = input.TargetLanguageIds
        });
        
        return client.ExecuteRequestAsync(request);
    }
    
    [Action("Add target language to project", Description = "Add one more target language to a specific project")]
    public async Task AddTargetLanguageToProject(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] AddTargetLanguagesToProjectRequest input)
    {
        var project = await GetProject(creds, input.ProjectId);

        var projectTargLangs = project.TargetLanguageIds ?? Enumerable.Empty<int>();
        var request = new UpdateProjectTargetLanguagesRequest()
        {
            ProjectId = input.ProjectId,
            TargetLanguageIds = projectTargLangs.Append(input.TargetLanguageId)
        };

        await UpdateTargetLanguagesForProject(creds, request);
    }

    [Action("Update specialization for a project", Description = "Update specialization for a specific project")]
    public void UpdateSpecializationForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Specialization ID")]
        string specializationId)
    {
        if (!int.TryParse(specializationId, out var intSpecializationId))
            throw new("Specialization ID must be a number");

        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/specialization", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            specializationId = intSpecializationId
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Update vendor instructions for a project",
        Description = "Update vendor instructions for a specific project")]
    public void UpdateVendorInstructionsForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Vendor instructions")]
        string vendorInstructions)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/vendorInstructions", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            value = vendorInstructions
        });
        client.ExecuteRequest<object>(request);
    }

    [Action("Update volume for a project", Description = "Update volume for a specific project")]
    public void UpdateVolumeForProject(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] [Display("Project ID")]
        string projectId,
        [ActionParameter] [Display("Volume")] int volume)
    {
        var client = new XtrfClient(authenticationCredentialsProviders);
        var request = new XtrfRequest("/v2/projects/" + projectId + "/volume", Method.Put,
            authenticationCredentialsProviders);
        request.AddJsonBody(new
        {
            value = volume
        });
        client.ExecuteRequest<object>(request);
    }
}