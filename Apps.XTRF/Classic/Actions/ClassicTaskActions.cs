using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Identifiers;
using Apps.XTRF.Classic.Models.Requests.ClassicTask;
using Apps.XTRF.Classic.Models.Responses.ClassicTask;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Classic.Actions;

[ActionList]
public class ClassicTaskActions : XtrfInvocable
{
    public ClassicTaskActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    #region Get

    [Action("Classic: Get task in project", Description = "Retrieve information about a task within the project")]
    public async Task<TaskResponse> GetTaskInProject([ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] ClassicTaskIdentifier taskIdentifier)
    {
        var task = await GetTask(projectIdentifier, taskIdentifier);
        return new(task);
    }
    
    [Action("Classic: Get task in quote", Description = "Retrieve information about a task within the quote")]
    public async Task<TaskResponse> GetTaskInQuote([ActionParameter] QuoteIdentifier quoteIdentifier, 
        [ActionParameter] ClassicTaskIdentifier taskIdentifier)
    {
        var task = await GetTask(quoteIdentifier, taskIdentifier);
        return new(task);
    }

    [Action("Classic: Get task progress", Description = "Get progress of a given task which contains information about task's " +
                                               "status (ie. opened or ready) and current phase (ie. translation)")]
    public async Task<TaskProgressResponse> GetTaskProgress([ActionParameter] ClassicTaskIdentifier taskIdentifier)
    {
        var request = new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/progress", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<TaskProgressResponse>(request);
        return response;
    }
    
    [Action("Classic: List task's files", Description = "List input (workfiles, translation memory, terminology, " +
                                                        "reference and log files) and output files")]
    public async Task<ListFilesResponse> ListTaskFiles([ActionParameter] ClassicTaskIdentifier taskIdentifier)
    {
        var request = new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/files", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ListFilesResponseWrapper>(request);
        return new(response);
    }
    
    private async Task<ClassicTask> GetTask(ProjectIdentifier projectIdentifier, ClassicTaskIdentifier taskIdentifier)
    {
        var request = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}?embed=tasks", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        var task = project.Tasks!.First(t => t.Id == taskIdentifier.TaskId);
        return task;
    }
    
    private async Task<ClassicTask> GetTask(QuoteIdentifier quoteIdentifier, ClassicTaskIdentifier taskIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}?embed=tasks", Method.Get, Creds);
        var quote = await Client.ExecuteWithErrorHandling<ClassicQuote>(request);
        var task = quote.Tasks!.First(t => t.Id == taskIdentifier.TaskId);
        return task;
    }
    
    #endregion

    #region Post

    [Action("Classic: Upload file for task", Description = "Upload a file for a task.")]
    public async Task<ClassicTaskIdentifier> UploadFileForTask([ActionParameter] ClassicTaskIdentifier taskIdentifier, 
        [ActionParameter] AddFileToTaskRequest input)
    {
        var uploadFileRequest = new XtrfRequest("/files", Method.Post, Creds);
        uploadFileRequest.AddFile("file", input.File.Bytes, input.File.Name);
        var uploadFileResponse = await Client.ExecuteWithErrorHandling<TokenResponse>(uploadFileRequest);
        
        var addFileToTaskRequest = new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/files/input", Method.Post, Creds)
            .WithJsonBody(new
            {
                token = uploadFileResponse.Token,
                category = input.Category
            });
        await Client.ExecuteWithErrorHandling(addFileToTaskRequest);
        return taskIdentifier;
    }

    [Action("Classic: Start task", Description = "Start a task.")]
    public async Task<ClassicTaskIdentifier> StartTask([ActionParameter] ClassicTaskIdentifier taskIdentifier)
    {
        var request = new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/start", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(request);
        return taskIdentifier;
    }
    
    [Action("Classic: Create task for project", Description = "Creates a new task for a given classic project")]
    public async Task<TaskResponse> CreateTaskForProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] LanguageCombinationIdentifier languageCombination,
        [ActionParameter] CreateTaskRequest input)
    {
        var task = await CreateTask($"/projects/{projectIdentifier.ProjectId}/tasks", languageCombination, input);
        return new(task);
    }
    
    [Action("Classic: Create task for quote", Description = "Creates a new task for a given classic quote")]
    public async Task<TaskResponse> CreateTaskForQuote([ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] LanguageCombinationIdentifier languageCombination,
        [ActionParameter] CreateTaskRequest input)
    {
        var task = await CreateTask($"/quotes/{quoteIdentifier.QuoteId}/tasks", languageCombination, input);
        return new(task);
    }
    
    private async Task<ClassicTask> CreateTask(string endpoint, LanguageCombinationIdentifier languageCombination, 
        CreateTaskRequest input)
    {
        var request = new XtrfRequest(endpoint, Method.Post, Creds)
            .WithJsonBody(new
            {
                specializationId = ConvertToInt64(input.SpecializationId, "Specialization"),
                workflowId = ConvertToInt64(input.WorkflowId, "Workflow ID"),
                name = input.Name,
                sourceLanguageId = ConvertToInt64(languageCombination.SourceLanguageId, "Source language"),
                targetLanguagesId = ConvertToInt64(languageCombination.TargetLanguageId, "Target language"),
                dates = new
                {
                    deadline = new
                    {
                        time = input.Deadline?.ConvertToUnixTime()
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
        
        var task = await Client.ExecuteWithErrorHandling<ClassicTask>(request);
        return task;
    }
    
    #endregion
    
    #region Put

    [Action("Classic: Update task in project", Description = "Update a task within the project, specifying only the " +
                                                             "fields that require updating")]
    public async Task<TaskResponse> UpdateTaskInProject([ActionParameter] ProjectIdentifier projectIdentifier,
        [ActionParameter] ClassicTaskIdentifier taskIdentifier, [ActionParameter] UpdateTaskRequest input)
    {
        var task = await GetTask(projectIdentifier, taskIdentifier);
        await UpdateTask(taskIdentifier, input, task);
        return new(task);
    }

    [Action("Classic: Update task in quote", Description = "Update a task within the quote, specifying only the " +
                                                           "fields that require updating")]
    public async Task<TaskResponse> UpdateTaskInProject([ActionParameter] QuoteIdentifier quoteIdentifier,
        [ActionParameter] ClassicTaskIdentifier taskIdentifier, [ActionParameter] UpdateTaskRequest input)
    {
        var task = await GetTask(quoteIdentifier, taskIdentifier);
        await UpdateTask(taskIdentifier, input, task);
        return new(task);
    }
    
    private async Task UpdateTask(ClassicTaskIdentifier taskIdentifier, UpdateTaskRequest input, ClassicTask task)
    {
        if (input.Name != null)
        {
            var updateNameRequest = new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/name", Method.Put, Creds)
                .WithJsonBody(new { value = input.Name });
            await Client.ExecuteWithErrorHandling(updateNameRequest);
            task.Name = input.Name;
        }
        
        if (input.ClientTaskPONumber != null)
        {
            var updateClientTaskPONumberRequest = 
                new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/clientTaskPONumber", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientTaskPONumber });
            await Client.ExecuteWithErrorHandling(updateClientTaskPONumberRequest);
            task.ClientTaskPONumber = input.ClientTaskPONumber;
        }
        
        if (input.PrimaryId != null || input.AdditionalIds != null || input.SendBackToId != null)
        {
            var jsonBody = new
            {
                primaryId = ConvertToInt64(
                    input.PrimaryId ?? task.People.CustomerContacts.PrimaryId,
                    "Primary contact person"),
                sendBackToId = ConvertToInt64(
                    input.SendBackToId ?? task.People.CustomerContacts.SendBackToId,
                    "Send back to contact person"),
                additionalIds = ConvertToInt64Enumerable(
                    input.AdditionalIds ?? task.People.CustomerContacts.AdditionalIds,
                    "Additional contact persons")
            };
            
            var updateContactsRequest =
                new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/contacts", Method.Put, Creds)
                    .WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateContactsRequest);
            
            task.People.CustomerContacts.PrimaryId = jsonBody.primaryId.ToString();
            task.People.CustomerContacts.SendBackToId = jsonBody.sendBackToId.ToString();
            task.People.CustomerContacts.AdditionalIds = jsonBody.additionalIds?.Select(id => id.ToString());
        }
        
        if (input.StartDate != null || input.Deadline != null || input.ActualStartDate != null ||
            input.ActualDeliveryDate != null)
        {
            var jsonBody = new
            {
                startDate = new
                {
                    time = input.StartDate == null
                        ? task.Dates.StartDate?.Time
                        : input.StartDate?.ConvertToUnixTime()
                },
                deadline = new
                {
                    time = input.Deadline == null
                        ? task.Dates.Deadline?.Time
                        : input.Deadline?.ConvertToUnixTime()
                },
                actualStartDate = new
                {
                    time = input.ActualStartDate == null
                        ? task.Dates.ActualStartDate?.Time
                        : input.ActualStartDate?.ConvertToUnixTime()
                },
                actualDeliveryDate = new
                {
                    time = input.ActualDeliveryDate == null
                        ? task.Dates.ActualDeliveryDate?.Time
                        : input.ActualDeliveryDate?.ConvertToUnixTime()
                }
            };
            
            var updateDatesRequest =
                new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/dates", Method.Put, Creds).WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateDatesRequest);

            task.Dates.StartDate = new(jsonBody.startDate.time);
            task.Dates.Deadline = new(jsonBody.deadline.time);
            task.Dates.ActualStartDate = new(jsonBody.actualStartDate.time);
            task.Dates.ActualDeliveryDate = new(jsonBody.actualDeliveryDate.time);
        }
        
        if (input.InstructionFromCustomer != null || input.InstructionForProvider != null ||
            input.InternalInstruction != null || input.PaymentNoteForCustomer != null || input.Notes != null ||
            input.PaymentNoteForVendor != null)
        {
            var jsonBody = new
            {
                fromCustomer = input.InstructionFromCustomer ?? task.Instructions.FromCustomer,
                forProvider = input.InstructionForProvider ?? task.Instructions.ForProvider,
                Internal = input.InternalInstruction ?? task.Instructions.Internal,
                paymentNoteForCustomer = input.PaymentNoteForCustomer ?? task.Instructions.PaymentNoteForCustomer,
                paymentNoteForVendor = input.PaymentNoteForVendor ?? task.Instructions.PaymentNoteForVendor,
                notes = input.Notes ?? task.Instructions.Notes
            };
            
            var updateInstructionsRequest =
                new XtrfRequest($"/tasks/{taskIdentifier.TaskId}/instructions", Method.Put, Creds).WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateInstructionsRequest);

            task.Instructions.FromCustomer = jsonBody.fromCustomer;
            task.Instructions.ForProvider = jsonBody.forProvider;
            task.Instructions.Internal = jsonBody.Internal;
            task.Instructions.PaymentNoteForCustomer = jsonBody.paymentNoteForCustomer;
            task.Instructions.PaymentNoteForVendor = jsonBody.paymentNoteForVendor;
            task.Instructions.Notes = jsonBody.notes;
        }
    }
    
    #endregion
    
    #region Delete
    
    [Action("Classic: Delete task", Description = "Delete a task")]
    public async Task DeleteTask([ActionParameter] ClassicTaskIdentifier taskIdentifier)
    {
        var request = new XtrfRequest($"/tasks/{taskIdentifier.TaskId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
    
    #endregion
}