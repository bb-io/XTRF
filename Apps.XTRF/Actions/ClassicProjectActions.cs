﻿using System.Net.Mime;
using Apps.XTRF.Api;
using Apps.XTRF.Constants;
using Apps.XTRF.Extensions;
using Apps.XTRF.Invocables;
using Apps.XTRF.Models.Identifiers;
using Apps.XTRF.Models.Requests.ClassicProject;
using Apps.XTRF.Models.Responses.ClassicProject;
using Apps.XTRF.Models.Responses.Entities;
using Apps.XTRF.Models.Responses.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;
using ProjectResponse = Apps.XTRF.Models.Responses.ClassicProject.ProjectResponse;

namespace Apps.XTRF.Actions;

[ActionList]
public class ClassicProjectActions : XtrfInvocable
{
    public ClassicProjectActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #region Get
    
    [Action("Classic: Get project details", Description = "Get information about classic project")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectIdentifier project)
    {
        var request = new XtrfRequest($"/projects/{project.ProjectId}?embed=tasks", Method.Get, Creds);
        var classicProject = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        var result = new ProjectResponse(classicProject);
        return result;
    }
    
    [Action("Classic: Download file", Description = "Download the content of a specific file in a classic project")]
    public async Task<DownloadFileResponse> DownloadFile([ActionParameter] FileIdentifier file)
    {
        var request = new XtrfRequest($"/projects/files/{file.FileId}/download", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling(request);
        var filename = response.ContentHeaders.First(h => h.Name == "Content-Disposition").Value.ToString().Split('"')[1];
    
        return new()
        {
            File = new(response.RawBytes)
            {
                Name = filename,
                ContentType = response.ContentType ?? MediaTypeNames.Application.Octet
            }
        };
    }

    #endregion

    #region Post

    [Action("Classic: Create new project", Description = "Create a new classic project")]
    public async Task<ProjectResponse> CreateProject([ActionParameter] CreateProjectRequest input)
    {
        var request = new XtrfRequest("/projects", Method.Post, Creds)
            .WithJsonBody(new
            {
                customerId = ConvertToInt64(input.CustomerId, "Customer ID"),
                serviceId = ConvertToInt64(input.ServiceId, "Service ID"),
                specializationId = ConvertToInt64(input.SpecializationId, "Specialization ID"),
                name = input.Name,
                sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                targetLanguagesIds = ConvertToInt64Enumerable(input.TargetLanguages, "Target languages"),
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
        
        var classicProject = await Client.ExecuteWithErrorHandling<ClassicProject>(request);
        var result = new ProjectResponse(classicProject);
        return result;
    }

    [Action("Classic: Create language combination for project", Description = "Create a new language combination for a " +
                                                                              "classic project without creating a task")]
    public async Task<ProjectIdentifier> CreateLanguageCombinationForProject([ActionParameter] ProjectIdentifier project, 
        [ActionParameter] CreateLanguageCombinationRequest input)
    {
        var request = new XtrfRequest($"/projects/{project.ProjectId}/languageCombinations", Method.Post, Creds)
            .WithJsonBody(new
            {
                sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                targetLanguagesIds = ConvertToInt64(input.TargetLanguageId, "Target language"),
            });
        await Client.ExecuteWithErrorHandling(request);
        return project;
    }

    [Action("Classic: Create task for project", Description = "Creates a new task for a given classic project")]
    public async Task<TaskResponse> CreateTaskForProject([ActionParameter] ProjectIdentifier project, 
        [ActionParameter] CreateTaskForProjectRequest input)
    {
        var request = new XtrfRequest($"/projects/{project.ProjectId}/tasks", Method.Post, Creds)
            .WithJsonBody(new
            {
                specializationId = ConvertToInt64(input.SpecializationId, "Specialization ID"),
                workflowId = ConvertToInt64(input.WorkflowId, "Workflow ID"),
                name = input.Name,
                sourceLanguageId = ConvertToInt64(input.SourceLanguageId, "Source language"),
                targetLanguagesIds = ConvertToInt64(input.TargetLanguageId, "Target language"),
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
        
        var task = await Client.ExecuteWithErrorHandling<ClassicProjectTask>(request);
        var result = new TaskResponse(task);
        return result;
    }
    
    #endregion

    #region Put

    [Action("Classic: Update project", Description = "Update a classic project. Specify only fields that need to be updated")]
    public async Task<ProjectIdentifier> UpdateProject([ActionParameter] ProjectIdentifier projectIdentifier, 
        [ActionParameter] UpdateProjectRequest input)
    {
        var getProjectRequest = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}", Method.Get, Creds);
        var project = await Client.ExecuteWithErrorHandling<ClassicProject>(getProjectRequest);
        
        if (input.PrimaryId != null || input.AdditionalIds != null || input.SendBackToId != null)
        {
            var updateContactsRequest = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/contacts", Method.Put, Creds)
                .WithJsonBody(new
                {
                    primaryId = ConvertToInt64(input.PrimaryId ?? project.Contacts.PrimaryId, "Primary contact person"),
                    sendBackToId = ConvertToInt64(input.SendBackToId ?? project.Contacts.SendBackToId, "Send back to contact person"),
                    additionalIds = ConvertToInt64Enumerable(input.AdditionalIds ?? project.Contacts.AdditionalIds, "Additional contact persons")
                }, JsonConfig.Settings);
            await Client.ExecuteWithErrorHandling(updateContactsRequest);
        }

        if (input.StartDate != null || input.Deadline != null || input.ActualStartDate != null ||
            input.ActualDeliveryDate != null)
        {
            var updateDatesRequest = new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/contacts", Method.Put, Creds)
                .WithJsonBody(new
                {
                    startDate = new
                    {
                        time = input.StartDate == null ? project.Dates.StartDate?.Time : input.StartDate?.ConvertToUnixTime()
                    },
                    deadline = new
                    {
                        time = input.Deadline == null ? project.Dates.Deadline?.Time : input.Deadline?.ConvertToUnixTime()
                    },
                    actualStartDate = new
                    {
                        time = input.ActualStartDate == null ? project.Dates.ActualStartDate?.Time : input.ActualStartDate?.ConvertToUnixTime()
                    },
                    actualDeliveryDate = new
                    {
                        time = input.ActualDeliveryDate == null ? project.Dates.ActualDeliveryDate?.Time : input.ActualDeliveryDate?.ConvertToUnixTime()
                    }
                }, JsonConfig.Settings);
            await Client.ExecuteWithErrorHandling(updateDatesRequest);
        }

        if (input.InstructionFromCustomer != null || input.InstructionForProvider != null ||
            input.InternalInstruction != null || input.PaymentNoteForCustomer != null || input.Notes != null)
        {
            var updateInstructionsRequest =
                new XtrfRequest($"/projects/{projectIdentifier.ProjectId}/instructions", Method.Put, Creds)
                    .WithJsonBody(new
                    {
                        fromCustomer = input.InstructionFromCustomer ?? project.Instructions.FromCustomer,
                        forProvider = input.InstructionForProvider ?? project.Instructions.ForProvider,
                        Internal = input.InternalInstruction ?? project.Instructions.Internal,
                        paymentNoteForCustomer = input.PaymentNoteForCustomer ?? project.Instructions.PaymentNoteForCustomer,
                        notes = input.Notes ?? project.Instructions.Notes
                    }, JsonConfig.Settings);
            await Client.ExecuteWithErrorHandling(updateInstructionsRequest);
        }

        return projectIdentifier;
    }
    
    #endregion
    
    #region Delete

    [Action("Classic: Delete project", Description = "Delete a classic project.")]
    public async Task DeleteProject([ActionParameter] ProjectIdentifier project)
    {
        var request = new XtrfRequest($"/projects/{project.ProjectId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    #endregion
}