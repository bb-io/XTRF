using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Identifiers;
using Apps.XTRF.Classic.Models.Requests.ClassicTask;
using Apps.XTRF.Classic.Models.Responses.ClassicTask;
using Apps.XTRF.Shared.Api;
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
    public async Task<TaskResponse> GetTaskInProject([ActionParameter] ProjectIdentifier project, 
        [ActionParameter] ClassicTaskIdentifier task)
    {
        var targetTask = await GetTask(project, task);
        return new(targetTask);
    }
    
    [Action("Classic: Get task in quote", Description = "Retrieve information about a task within the quote")]
    public async Task<TaskResponse> GetTaskInQuote([ActionParameter] QuoteIdentifier quote, 
        [ActionParameter] ClassicTaskIdentifier task)
    {
        var targetTask = await GetTask(quote, task);
        return new(targetTask);
    }

    [Action("Classic: Get task progress", Description = "Get progress of a given task which contains information about task's " +
                                               "status (ie. opened or ready) and current phase (ie. translation)")]
    public async Task<TaskProgressResponse> GetTaskProgress([ActionParameter] ClassicTaskIdentifier task)
    {
        var request = new XtrfRequest($"/tasks/{task.TaskId}/progress", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<TaskProgressResponse>(request);
        return response;
    }
    
    [Action("Classic: List task's files", Description = "List input (workfiles, translation memory, terminology, " +
                                                        "reference and log files) and output files")]
    public async Task<ListFilesResponse> ListTaskFiles([ActionParameter] ClassicTaskIdentifier task)
    {
        var request = new XtrfRequest($"/tasks/{task.TaskId}/files", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ListFilesResponseWrapper>(request);
        return new(response);
    }
    
    #endregion

    #region Post

    [Action("Classic: Upload file for task", Description = "Upload a file for a task.")]
    public async Task<ClassicTaskIdentifier> UploadFileForTask([ActionParameter] ClassicTaskIdentifier task, 
        [ActionParameter] AddFileToTaskRequest input)
    {
        var uploadFileRequest = new XtrfRequest("/files", Method.Post, Creds);
        uploadFileRequest.AddFile("file", input.File.Bytes, input.File.Name);
        var uploadFileResponse = await Client.ExecuteWithErrorHandling<TokenResponse>(uploadFileRequest);
        
        var addFileToTaskRequest = new XtrfRequest($"/tasks/{task.TaskId}/files/input", Method.Post, Creds)
            .WithJsonBody(new
            {
                token = uploadFileResponse.Token,
                category = input.Category
            });
        await Client.ExecuteWithErrorHandling(addFileToTaskRequest);
        return task;
    }

    [Action("Classic: Start task", Description = "Start a task.")]
    public async Task<ClassicTaskIdentifier> StartTask([ActionParameter] ClassicTaskIdentifier task)
    {
        var request = new XtrfRequest($"/tasks/{task.TaskId}/start", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(request);
        return task;
    }
    
    #endregion
    
    #region Put

    [Action("Classic: Update task in project", Description = "Update a task within the project, specifying only the " +
                                                             "fields that require updating")]
    public async Task<TaskResponse> UpdateTaskInProject([ActionParameter] ProjectIdentifier project,
        [ActionParameter] ClassicTaskIdentifier task, [ActionParameter] UpdateTaskRequest input)
    {
        var targetTask = await GetTask(project, task);
        await UpdateTask(task, input, targetTask);
        return new(targetTask);
    }

    [Action("Classic: Update task in quote", Description = "Update a task within the quote, specifying only the " +
                                                           "fields that require updating")]
    public async Task<TaskResponse> UpdateTaskInProject([ActionParameter] QuoteIdentifier quote,
        [ActionParameter] ClassicTaskIdentifier task, [ActionParameter] UpdateTaskRequest input)
    {
        var targetTask = await GetTask(quote, task);
        await UpdateTask(task, input, targetTask);
        return new(targetTask);
    }
    
    #endregion
    
    #region Delete
    
    [Action("Classic: Delete task", Description = "Delete a task.")]
    public async Task DeleteTask([ActionParameter] ClassicTaskIdentifier task)
    {
        var request = new XtrfRequest($"/tasks/{task.TaskId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
    
    #endregion

    private async Task<ClassicTask> GetTask(ProjectIdentifier project, ClassicTaskIdentifier task)
    {
        var request = new XtrfRequest($"/projects/{project.ProjectId}?embed=tasks", Method.Get, Creds);
        var classicProject = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        var targetTask = classicProject.Tasks!.First(t => t.Id == task.TaskId);
        return targetTask;
    }
    
    private async Task<ClassicTask> GetTask(QuoteIdentifier quote, ClassicTaskIdentifier task)
    {
        var request = new XtrfRequest($"/quotes/{quote.QuoteId}?embed=tasks", Method.Get, Creds);
        var classicQuote = await Client.ExecuteWithErrorHandling<ClassicQuote>(request);
        var targetTask = classicQuote.Tasks!.First(t => t.Id == task.TaskId);
        return targetTask;
    }
    
    private async Task UpdateTask(ClassicTaskIdentifier task, UpdateTaskRequest input, ClassicTask targetTask)
    {
        if (input.Name != null)
        {
            var updateNameRequest = new XtrfRequest($"/tasks/{task.TaskId}/name", Method.Put, Creds)
                .WithJsonBody(new { value = input.Name });
            await Client.ExecuteWithErrorHandling(updateNameRequest);
            targetTask.Name = input.Name;
        }
        
        if (input.ClientTaskPONumber != null)
        {
            var updateClientTaskPONumberRequest = 
                new XtrfRequest($"/tasks/{task.TaskId}/clientTaskPONumber", Method.Put, Creds)
                    .WithJsonBody(new { value = input.ClientTaskPONumber });
            await Client.ExecuteWithErrorHandling(updateClientTaskPONumberRequest);
            targetTask.ClientTaskPONumber = input.ClientTaskPONumber;
        }
        
        if (input.PrimaryId != null || input.AdditionalIds != null || input.SendBackToId != null)
        {
            var jsonBody = new
            {
                primaryId = ConvertToInt64(
                    input.PrimaryId ?? targetTask.People.CustomerContacts.PrimaryId,
                    "Primary contact person"),
                sendBackToId = ConvertToInt64(
                    input.SendBackToId ?? targetTask.People.CustomerContacts.SendBackToId,
                    "Send back to contact person"),
                additionalIds = ConvertToInt64Enumerable(
                    input.AdditionalIds ?? targetTask.People.CustomerContacts.AdditionalIds,
                    "Additional contact persons")
            };
            
            var updateContactsRequest =
                new XtrfRequest($"/tasks/{task.TaskId}/contacts", Method.Put, Creds)
                    .WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateContactsRequest);
            
            targetTask.People.CustomerContacts.PrimaryId = jsonBody.primaryId.ToString();
            targetTask.People.CustomerContacts.SendBackToId = jsonBody.sendBackToId.ToString();
            targetTask.People.CustomerContacts.AdditionalIds = jsonBody.additionalIds?.Select(id => id.ToString());
        }
        
        if (input.StartDate != null || input.Deadline != null || input.ActualStartDate != null ||
            input.ActualDeliveryDate != null)
        {
            var jsonBody = new
            {
                startDate = new
                {
                    time = input.StartDate == null
                        ? targetTask.Dates.StartDate?.Time
                        : input.StartDate?.ConvertToUnixTime()
                },
                deadline = new
                {
                    time = input.Deadline == null
                        ? targetTask.Dates.Deadline?.Time
                        : input.Deadline?.ConvertToUnixTime()
                },
                actualStartDate = new
                {
                    time = input.ActualStartDate == null
                        ? targetTask.Dates.ActualStartDate?.Time
                        : input.ActualStartDate?.ConvertToUnixTime()
                },
                actualDeliveryDate = new
                {
                    time = input.ActualDeliveryDate == null
                        ? targetTask.Dates.ActualDeliveryDate?.Time
                        : input.ActualDeliveryDate?.ConvertToUnixTime()
                }
            };
            
            var updateDatesRequest =
                new XtrfRequest($"/tasks/{task.TaskId}/dates", Method.Put, Creds).WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateDatesRequest);

            targetTask.Dates.StartDate = new() { Time = jsonBody.startDate.time };
            targetTask.Dates.Deadline = new() { Time = jsonBody.deadline.time };
            targetTask.Dates.ActualStartDate = new() { Time = jsonBody.actualStartDate.time };
            targetTask.Dates.ActualDeliveryDate = new() { Time = jsonBody.actualDeliveryDate.time };
        }
        
        if (input.InstructionFromCustomer != null || input.InstructionForProvider != null ||
            input.InternalInstruction != null || input.PaymentNoteForCustomer != null || input.Notes != null ||
            input.PaymentNoteForVendor != null)
        {
            var jsonBody = new
            {
                fromCustomer = input.InstructionFromCustomer ?? targetTask.Instructions.FromCustomer,
                forProvider = input.InstructionForProvider ?? targetTask.Instructions.ForProvider,
                Internal = input.InternalInstruction ?? targetTask.Instructions.Internal,
                paymentNoteForCustomer = input.PaymentNoteForCustomer ?? targetTask.Instructions.PaymentNoteForCustomer,
                paymentNoteForVendor = input.PaymentNoteForVendor ?? targetTask.Instructions.PaymentNoteForVendor,
                notes = input.Notes ?? targetTask.Instructions.Notes
            };
            
            var updateInstructionsRequest =
                new XtrfRequest($"/tasks/{task.TaskId}/instructions", Method.Put, Creds).WithJsonBody(jsonBody);
            await Client.ExecuteWithErrorHandling(updateInstructionsRequest);

            targetTask.Instructions.FromCustomer = jsonBody.fromCustomer;
            targetTask.Instructions.ForProvider = jsonBody.forProvider;
            targetTask.Instructions.Internal = jsonBody.Internal;
            targetTask.Instructions.PaymentNoteForCustomer = jsonBody.paymentNoteForCustomer;
            targetTask.Instructions.PaymentNoteForVendor = jsonBody.paymentNoteForVendor;
            targetTask.Instructions.Notes = jsonBody.notes;
        }
    }
}