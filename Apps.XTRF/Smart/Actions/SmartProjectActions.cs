using Apps.XTRF.Shared.Actions.Base;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Extensions;
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
using Apps.XTRF.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Globalization;
using System.Net;
using System.Net.Mime;

namespace Apps.XTRF.Smart.Actions;

[ActionList("Smart: projects")]
public class SmartProjectActions : BaseFileActions
{
    public SmartProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
        : base(invocationContext, fileManagementClient)
    {
    }

    #region Get

    [Action("Smart: Get project details", Description =
        "Get information about a smart project. If you need to retrieve " +
        "client contacts, finance information, process ID or check if " +
        "project created in CAT tool or creation is queued, set the " +
        "respective optional parameter to 'True'")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] [Display("Include client contacts")]
        bool? includeClientContacts,
        [ActionParameter] [Display("Include finance information")]
        bool? includeFinanceInformation,
        [ActionParameter] [Display("Include process ID")]
        bool? includeProcessId,
        [ActionParameter] [Display("Include if project is created in CAT tool or queued")]
        bool? includeCatTool)
    {
        var getProjectRequest = new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<SmartProject>(getProjectRequest);
        var timeZoneInfo = await GetTimeZoneInfo();
        var projectResponse = new ProjectResponse(project, timeZoneInfo);

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
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/catToolProject", Method.Get, Creds);
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
        var timeZoneInfo = await GetTimeZoneInfo();
        return new(jobs.Select(job => new JobResponse(job, timeZoneInfo)));
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

    [Action("Smart: List deliverable files for source file", Description =
        "List files ready for delivery with the same " +
        "name as given source file in a smart project")]
    public async Task<ListFilesResponse> GetDeliverableFilesForSourceFile(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] FileIdentifier fileIdentifier,
        [ActionParameter] FilterLanguageOptionalIdentifier languageIdentifier)
    {
        var getDeliverableFilesRequest =
            new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/files/deliverable", Method.Get, Creds);
        var deliverableFiles =
            (await Client.ExecuteWithErrorHandling<IEnumerable<SmartFileXTRF>>(getDeliverableFilesRequest)).ToList();

        if (deliverableFiles.Count == 0)
            return new(deliverableFiles);

        var getFileRequest = new XtrfRequest($"/v2/projects/files/{fileIdentifier.FileId}", Method.Get, Creds);
        var file = await Client.ExecuteWithErrorHandling<SmartFileXTRF>(getFileRequest);

        var filteredDeliverableFiles = deliverableFiles
            .Where(deliverableFile => deliverableFile.Name == file.Name)
            .Where(deliverableFile => languageIdentifier.LanguageId == null ||
                                      deliverableFile.LanguageRelation.Languages.Contains(languageIdentifier
                                          .LanguageId));

        return new(filteredDeliverableFiles);
    }

    [Action("Smart: Download file", Description = "Download the content of a specific file")]
    public async Task<FileWrapper> DownloadFile([ActionParameter] FileIdentifier fileIdentifier,
        [ActionParameter] [Display("Filename")]
        string filename)
    {
        filename = filename.Trim();
        var request = new XtrfRequest($"/v2/projects/files/{fileIdentifier.FileId}/download/{filename}", Method.Get,
            Creds);
        var response = await Client.ExecuteWithErrorHandling(request);

        var contentType = DownloadContentTypeHelper.Resolve(response.ContentType, filename);

        var fileReference = await UploadFile(response.RawBytes, contentType, filename);

        return new() { File = fileReference };
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

    [Action("Smart: Add receivable to project", Description = "Add a simple receivable to a smart project")]
    public async Task<SmartReceivableIdentifier> AddReceivableToProject(
         [ActionParameter] ProjectIdentifier projectIdentifier,
         [ActionParameter] AddReceivableRequest input)
    {
        var request =
            new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/finance/receivables", Method.Post, Creds)
                .WithJsonBody(new
                {
                    id = string.IsNullOrWhiteSpace(input.Id) ? null : ConvertToInt64(input.Id, "Receivable ID"),
                    jobTypeId = ConvertToInt64(input.JobTypeId, "Job type"),
                    languageCombination = new
                    {
                        sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                        targetLanguageId = ConvertToInt64(input.TargetLanguageId, "Target language")
                    },
                    rateOrigin = input.RateOrigin ?? "PRICE_PROFILE",
                    currencyId = ConvertToInt64(input.CurrencyId, "Currency"),
                    total = ConvertToInt64(input.Total, "Total"),
                    invoiceId = input.InvoiceId,
                    type = input.Type ?? "SIMPLE",
                    calculationUnitId = ConvertToInt64(input.CalculationUnitId, "Calculation unit"),
                    ignoreMinimumCharge = input.IgnoreMinimumCharge ?? true,
                    minimumCharge = input.MinimumCharge ?? 0,
                    description = input.Description,
                    rate = input.Rate,
                    quantity = input.Quantity ?? 0
                }, JsonConfig.Settings);

        var dto = await Client.ExecuteWithErrorHandling<ReceivableIdDto>(request);

        return new SmartReceivableIdentifier
        {
            ReceivableId = dto.Id
        };
    }

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
        var timeZoneInfo = await GetTimeZoneInfo();
        return new(project, timeZoneInfo);
    }

    [Action("Smart: Upload file to project", Description = "Upload a file to a smart project")]
    public async Task<FileIdentifier> UploadFileToProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] UploadFileRequest input)
    {
        var uploadFileRequest =
            new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/files/upload", Method.Post, Creds);
        var fileBytes = await DownloadFile(input.File);
        uploadFileRequest.AddFile("file", fileBytes, input.File.Name!.Sanitize());
        var fileIdentifier = await Client.ExecuteWithErrorHandling<FileIdentifier>(uploadFileRequest);

        var addFileRequest = new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/files/add",
                Method.Put, Creds)
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

    [Action("Smart: Create receivable for project", Description = "Create a receivable for a smart project")]
    public async Task<UploadedFinanceFileResponse> CreateReceivableForProject(
        [ActionParameter] CreateReceivableRequest input)
    {
        string fileName = input.FileName ?? input.File.Name!;
        var fileBytes = await DownloadFile(input.File);
        var fileUploadedResponse = await UploadFile(fileBytes, fileName);

        var body = new Dictionary<string, object?>
        {
            ["id"] = input.Id == null ? null : ConvertToInt64(input.Id, "Receivable ID"),
            ["jobTypeId"] = ConvertToInt64(input.JobType, "Job type"),
            ["languageCombination"] = new
            {
                sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                targetLanguageId = ConvertToInt64(input.TargetLanguageId, "Target language")
            },
            ["rateOrigin"] = input.RateOrigin ?? "PRICE_PROFILE",
            ["currencyId"] = ConvertToInt64(input.CurrencyId, "Currency"),
            ["invoiceId"] = input.InvoiceId,
            ["type"] = input.Type ?? "CAT",
            ["calculationUnitId"] = ConvertToInt64(input.CalculationUnitId, "Calculation unit"),
            ["description"] = input.Description,

            ["catLogFile"] = new
            {
                name = fileName,
                token = fileUploadedResponse.Token
            }
        };

        if (input.IgnoreMinimumChange is not null)
            body["ignoreMinimumCharge"] = input.IgnoreMinimumChange.Value;

        if (input.MinimumCharge is not null)
            body["minimumCharge"] = input.MinimumCharge.Value;

        if (!string.IsNullOrWhiteSpace(input.Total))
            body["total"] = ConvertToDecimal(input.Total, "Total");

        if (!string.IsNullOrWhiteSpace(input.Rate))
            body["rate"] = ConvertToDecimal(input.Rate, "Rate");

        if (input.Quantity is not null)
            body["quantity"] = input.Quantity.Value;
        if (!string.IsNullOrWhiteSpace(input.TaskId))
            body["taskId"] = ConvertToInt64(input.TaskId, "Task ID");

        var request = new XtrfRequest($"/v2/projects/{input.ProjectId}/finance/receivables", Method.Post, Creds)
            .WithJsonBody(body);

        var dto = await Client.ExecuteWithErrorHandling<UploadedFinanceFileDto>(request);
        return new UploadedFinanceFileResponse(dto);
    }


    private static decimal ConvertToDecimal(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new PluginMisconfigurationException($"{fieldName} is required.");

        var normalized = value.Trim().Replace(',', '.');

        if (!decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
            throw new PluginMisconfigurationException($"Invalid {fieldName}: '{value}'");

        return result;
    }

    [Action("Smart: Create payable for project", Description = "Create a payable for a smart project")]
    public async Task<UploadedFinanceFileResponse> CreatePayableForProject(
        [ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] CreatePayableRequest input)
    {
        string fileName = input.FileName ?? input.File.Name!;
        var fileBytes = await DownloadFile(input.File);
        var fileUploadedResponse = await UploadFile(fileBytes, fileName);

        var createPayableRequest =
            new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/finance/payables", Method.Post, Creds)
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

    [Action("Smart: Update project",
        Description = "Update a smart project, specifying only the fields that require updating")]
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
            var timeZoneInfo = await GetTimeZoneInfo();
            var updateClientDeadlineRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/clientDeadline", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientDeadline?.ConvertToUnixTime(timeZoneInfo) });

            await Client.ExecuteWithErrorHandling(updateClientDeadlineRequest);
        }

        if (input.OrderDate != null)
        {
            var timeZoneInfo = await GetTimeZoneInfo();
            var updateClientDeadlineRequest =
                new XtrfRequest($"/v2/projects/{projectIdentifier.ProjectId}/orderDate", Method.Put, Creds)
                    .WithJsonBody(new { value = input.OrderDate?.ConvertToUnixTime(timeZoneInfo) });

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