using System.Net.Mime;
using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Identifiers;
using Apps.XTRF.Classic.Models.Requests.ClassicProject;
using Apps.XTRF.Classic.Models.Responses.ClassicProject;
using Apps.XTRF.Classic.Models.Responses.ClassicTask;
using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models;
using Apps.XTRF.Shared.Models.Identifiers;
using Apps.XTRF.Smart.Models.Requests.File;
using Apps.XTRF.Smart.Models.Responses.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.XTRF.Classic.Actions;

[ActionList]
public class ClassicProjectActions : BaseFileActions
{
    public ClassicProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext, fileManagementClient)
    {
    }

    #region Get
    
    [Action("Classic: Get project details", Description = "Get information about classic project")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectIdentifier projectIdentifier)
    {
        var request = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}?embed=tasks", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        var timeZoneInfo = await GetTimeZoneInfo();
        return new(project, timeZoneInfo);
    }
    
    [Action("Classic: Download file", Description = "Download the content of a specific file in a classic project")]
    public async Task<FileWrapper> DownloadFile([ActionParameter] ClassicTaskIdentifier taskIdentifier, 
        [ActionParameter] ClassicFileIdentifier fileIdentifier)
    {
        var request = new XtrfRequest($"/projects/files/{fileIdentifier.FileId}/download", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling(request);
        
        var getFilesRequest = new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/files", Method.Get, Creds);
        var taskFiles = await Client.ExecuteWithErrorHandling<JobFilesResponse>(getFilesRequest);
        var filesCombined = taskFiles.Jobs.SelectMany(job => job.Files.InputFiles.Concat(job.Files.OutputFiles));
        var targetFile = filesCombined.First(file => file.Id == fileIdentifier.FileId);
        
        var fileReference = await UploadFile(response.RawBytes,
            response.ContentType ?? MediaTypeNames.Application.Octet, targetFile.Name);
    
        return new() { File = fileReference };
    }

    #endregion

    #region Post

    [Action("Classic: Create new project", Description = "Create a new classic project")]
    public async Task<ProjectResponse> CreateProject([ActionParameter] CreateProjectRequest input)
    {
        var timeZoneInfo = await GetTimeZoneInfo();
        
        var request = new XtrfRequest("/projects", Method.Post, Creds)
            .WithJsonBody(new
            {
                customerId = ConvertToInt64(input.CustomerId, "Customer ID"),
                serviceId = ConvertToInt64(input.ServiceId, "Service ID"),
                specializationId = ConvertToInt64(input.SpecializationId, "Specialization"),
                input.Name,
                sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                targetLanguagesIds = ConvertToInt64Enumerable(input.TargetLanguages, "Target languages"),
                dates = new
                {
                    deadline = new
                    {
                        time = input.Deadline?.ConvertToUnixTime(timeZoneInfo)
                    }
                },
                instructions = new
                {
                    fromCustomer = input.InstructionFromCustomer,
                    forProvider = input.InstructionForProvider,
                    Internal = input.InternalInstruction,
                    paymentNoteForCustomer = input.PaymentNoteForCustomer,
                    notes = input.Notes 
                }
            }, JsonConfig.Settings);
        
        var project = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        var result = new ProjectResponse(project, timeZoneInfo);
        return result;
    }

    [Action("Classic: Create language combination for project", Description = "Create a new language combination for a " +                                                                              "classic project without creating a task")]
    public async Task<ProjectIdentifier> CreateLanguageCombinationForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] LanguageCombinationIdentifier languageCombination)
    {
        if (projectIdentifier is null)
            throw new PluginMisconfigurationException("Project is required. Please check your input and try again");
        if (string.IsNullOrWhiteSpace(projectIdentifier.ProjectId))
            throw new PluginMisconfigurationException("Project ID is required. Please check your input and try again");

        if (languageCombination is null)
            throw new PluginMisconfigurationException("Language combination is required. Please check your input and try again");

        var request = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/languageCombinations", Method.Post, Creds)
            .WithJsonBody(new
            {
                sourceLanguageId = ConvertToInt64(languageCombination.SourceLanguageId, "Source language"),
                targetLanguageId = ConvertToInt64(languageCombination.TargetLanguageId, "Target language")
            });
        await Client.ExecuteWithErrorHandling(request);
        return projectIdentifier;
    }
    
    [Action("Classic: Create payable for project", Description = "Create a payable for a classic project")]
    public async Task<UploadedFinanceFileResponse> CreatePayableForProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CreateClassicPayableRequest input)
    {
        string fileName = input.FileName ?? input.File.Name!;
        var fileBytes = await DownloadFile(input.File);
        var fileUploadedResponse = await UploadFile(fileBytes, fileName);

        var createPayableRequest =
            new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/finance/payables", Method.Post, Creds)
                .WithJsonBody(new
                {
                    id = input.Id == null ? null : ConvertToInt64(input.Id, "Payable ID"),
                    jobTypeId = ConvertToInt64(input.JobType, "Job type"),
                    languageCombination = new
                    {
                        sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                        targetLanguageId = ConvertToInt64(input.TargetLanguageId, "Target language")
                    },
                    rateOrigin = input.RateOrigin ?? "PRICE_PROFILE",
                    currencyId = ConvertToInt64(input.CurrencyId, "Currency"),
                    total = ConvertToInt64(input.Total, "Total"),
                    invoiceId = input.InvoiceId,
                    type = input.Type ?? "CAT",
                    calculationUnitId = ConvertToInt64(input.CalculationUnitId, "Calculation unit"),
                    ignoreMinimumCharge = input.IgnoreMinimumChange ?? true,
                    minimumCharge = input.MinimumCharge ?? 0,
                    description = input.Description,
                    rate = input.Rate,
                    quantity = input.Quantity ?? 0,
                    jobId = input.JobId,
                    catLogFile = new
                    {
                        name = fileName,
                        token = fileUploadedResponse.Token
                    }
                });

        var dto = await Client.ExecuteWithErrorHandling<UploadedFinanceFileDto>(createPayableRequest);
        return new(dto);
    }
    
    [Action("Classic: Create receivable for project", Description = "Create a receivable for a classic project")]
    public async Task<UploadedFinanceFileResponse> CreateReceivableForProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CreateReceivableClassicRequest input)
    {
        string fileName = input.FileName ?? input.File.Name!;
        var fileBytes = await DownloadFile(input.File);
        var fileUploadedResponse = await UploadFile(fileBytes, fileName);
        
        var createPayableRequest =
            new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/finance/receivables", Method.Post, Creds)
                .WithJsonBody(new
                {
                    id = input.Id == null ? null : ConvertToInt64(input.Id, "Payable ID"),
                    jobTypeId = ConvertToInt64(input.JobType, "Job type"),
                    languageCombination = new
                    {
                        sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                        targetLanguageId = ConvertToInt64(input.TargetLanguageId, "Target language")
                    },
                    rateOrigin = input.RateOrigin ?? "PRICE_PROFILE",
                    currencyId = ConvertToInt64(input.CurrencyId, "Currency"),
                    total = ConvertToInt64(input.Total, "Total"),
                    invoiceId = input.InvoiceId,
                    type = input.Type ?? "CAT",
                    calculationUnitId = ConvertToInt64(input.CalculationUnitId, "Calculation unit"),
                    ignoreMinimumCharge = input.IgnoreMinimumChange ?? true,
                    minimumCharge = input.MinimumCharge ?? 0,
                    description = input.Description,
                    rate = input.Rate,
                    quantity = input.Quantity ?? 0,
                    taskId = ConvertToInt64(input.TaskId, "Task ID"),
                    catLogFile = new
                    {
                        name = fileName,
                        token = fileUploadedResponse.Token
                    }
                });

        var dto = await Client.ExecuteWithErrorHandling<UploadedFinanceFileDto>(createPayableRequest);
        return new(dto);
    }

    #endregion

    #region Put

    [Action("Classic: Update project", Description = "Update a classic project, specifying only the fields that require updating")]
    public async Task<ProjectResponse> UpdateProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] UpdateProjectRequest input)
    {
        var getProjectRequest = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<ClassicProject>(getProjectRequest);
        var timeZoneInfo = await GetTimeZoneInfo();

        if (input.PrimaryId != null || input.AdditionalIds != null || input.SendBackToId != null)
        {
            var jsonBody = new
            {
                primaryId = ConvertToInt64(input.PrimaryId ?? project.Contacts.PrimaryId,
                    "Primary contact person"),
                sendBackToId = ConvertToInt64(input.SendBackToId ?? project.Contacts.SendBackToId,
                    "Send back to contact person"),
                additionalIds = ConvertToInt64Enumerable(input.AdditionalIds ?? project.Contacts.AdditionalIds,
                    "Additional contact persons")
            };
            
            var updateContactsRequest = 
                new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/contacts", Method.Put, Creds)
                    .WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateContactsRequest);

            project.Contacts.PrimaryId = jsonBody.primaryId.ToString();
            project.Contacts.SendBackToId = jsonBody.sendBackToId.ToString();
            project.Contacts.AdditionalIds = jsonBody.additionalIds?.Select(id => id.ToString());
        }

        if (input.StartDate != null || input.Deadline != null || input.ActualStartDate != null ||
            input.ActualDeliveryDate != null)
        {
            var jsonBody = new
            {
                startDate = new
                {
                    time = input.StartDate == null
                        ? project.Dates.StartDate?.Time
                        : input.StartDate?.ConvertToUnixTime(timeZoneInfo)
                },
                deadline = new
                {
                    time = input.Deadline == null
                        ? project.Dates.Deadline?.Time
                        : input.Deadline?.ConvertToUnixTime(timeZoneInfo)
                },
                actualStartDate = new
                {
                    time = input.ActualStartDate == null
                        ? project.Dates.ActualStartDate?.Time
                        : input.ActualStartDate?.ConvertToUnixTime(timeZoneInfo)
                },
                actualDeliveryDate = new
                {
                    time = input.ActualDeliveryDate == null
                        ? project.Dates.ActualDeliveryDate?.Time
                        : input.ActualDeliveryDate?.ConvertToUnixTime(timeZoneInfo)
                }
            };
            
            var updateDatesRequest =
                new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/contacts", Method.Put, Creds)
                    .WithJsonBody(jsonBody, JsonConfig.Settings);
            await Client.ExecuteWithErrorHandling(updateDatesRequest);
            
            project.Dates.StartDate = new(jsonBody.startDate.time);
            project.Dates.Deadline = new(jsonBody.deadline.time);
            project.Dates.ActualStartDate = new(jsonBody.actualStartDate.time);
            project.Dates.ActualDeliveryDate = new(jsonBody.actualDeliveryDate.time);
        }

        if (input.InstructionFromCustomer != null || input.InstructionForProvider != null ||
            input.InternalInstruction != null || input.PaymentNoteForCustomer != null || input.Notes != null ||
            input.PaymentNoteForVendor != null)
        {
            var jsonBody = new
            {
                fromCustomer = input.InstructionFromCustomer ?? project.Instructions.FromCustomer,
                forProvider = input.InstructionForProvider ?? project.Instructions.ForProvider,
                Internal = input.InternalInstruction ?? project.Instructions.Internal,
                paymentNoteForCustomer = input.PaymentNoteForCustomer ?? project.Instructions.PaymentNoteForCustomer,
                paymentNoteForVendor = input.PaymentNoteForVendor ?? project.Instructions.PaymentNoteForVendor,
                notes = input.Notes ?? project.Instructions.Notes
            };
            
            var updateInstructionsRequest =
                new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/instructions", Method.Put, Creds)
                    .WithJsonBody(jsonBody, JsonConfig.Settings);
            await Client.ExecuteWithErrorHandling(updateInstructionsRequest);
            
            project.Instructions.FromCustomer = jsonBody.fromCustomer;
            project.Instructions.ForProvider = jsonBody.forProvider;
            project.Instructions.Internal = jsonBody.Internal;
            project.Instructions.PaymentNoteForCustomer = jsonBody.paymentNoteForCustomer;
            project.Instructions.PaymentNoteForVendor = jsonBody.paymentNoteForVendor;
            project.Instructions.Notes = jsonBody.notes;
        }

        return new(project, timeZoneInfo);
    }

    #endregion
    
    #region Delete

    [Action("Classic: Delete project", Description = "Delete a classic project.")]
    public async Task DeleteProject([ActionParameter] ProjectIdentifier projectIdentifier)
    {
        var request = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    #endregion
}