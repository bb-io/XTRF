using Apps.XTRF.Classic.Models.Requests;
using Apps.XTRF.Classic.Models.Responses;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.XTRF.Shared.Actions;

[ActionList("Project group")]

public class ProjectGroupActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
: XtrfInvocable(invocationContext)
{

    [Action("Link project group to projects", Description = "Add projects to a project group")]
    public async Task<ProjectGroupDto> LinkProjects(
        [ActionParameter] ProjectGroupIdentifier projectGroupIdentifier,
        [ActionParameter] LinkProjectsRequest input)
    {
        ValidateProjectGroup(projectGroupIdentifier);
        ValidateProjectsInput(input);

        var request = new XtrfRequest(
            $"/projectGroups/{projectGroupIdentifier.ProjectGroupId}/linkProjects",
            Method.Put,
            Creds)
            .WithJsonBody(new
            {
                projectIds = ConvertToNullableInt64Enumerable(input.ProjectIds, "Project IDs"),
                smartProjectIds = input.SmartProjectIds
            });

        var response = await Client.ExecuteWithErrorHandling<ProjectGroupDto>(request);
        return response;
    }

    [Action("Unlink project group from projects", Description = "Remove projects from a project group")]
    public async Task<ProjectGroupDto> UnlinkProjects(
        [ActionParameter] ProjectGroupIdentifier projectGroupIdentifier,
        [ActionParameter] LinkProjectsRequest input)
    {
        ValidateProjectGroup(projectGroupIdentifier);
        ValidateProjectsInput(input);

        var request = new XtrfRequest(
            $"/projectGroups/{projectGroupIdentifier.ProjectGroupId}/unlinkProjects",
            Method.Put,
            Creds)
            .WithJsonBody(new
            {
                projectIds = ConvertToNullableInt64Enumerable(input.ProjectIds, "Project IDs"),
                smartProjectIds = input.SmartProjectIds
            });

        var response = await Client.ExecuteWithErrorHandling<ProjectGroupDto>(request);
        return response;
    }

    [Action("Link project group to quotes", Description = "Add quotes to a project group")]
    public async Task<ProjectGroupDto> LinkQuotes(
        [ActionParameter] ProjectGroupIdentifier projectGroupIdentifier,
        [ActionParameter] LinkQuotesRequest input)
    {
        ValidateProjectGroup(projectGroupIdentifier);
        ValidateQuotesInput(input);

        var request = new XtrfRequest(
            $"/projectGroups/{projectGroupIdentifier.ProjectGroupId}/linkQuotes",
            Method.Put,
            Creds)
            .WithJsonBody(new
            {
                quoteIds = ConvertToNullableInt64Enumerable(input.QuoteIds, "Quote IDs"),
                smartQuoteIds = input.SmartQuoteIds
            });

        var response = await Client.ExecuteWithErrorHandling<ProjectGroupDto>(request);
        return response;
    }

    [Action("Unlink project group from quotes", Description = "Remove quotes from a project group")]
    public async Task<ProjectGroupDto> UnlinkQuotes(
        [ActionParameter] ProjectGroupIdentifier projectGroupIdentifier,
        [ActionParameter] LinkQuotesRequest input)
    {
        ValidateProjectGroup(projectGroupIdentifier);
        ValidateQuotesInput(input);

        var request = new XtrfRequest(
            $"/projectGroups/{projectGroupIdentifier.ProjectGroupId}/unlinkQuotes",
            Method.Put,
            Creds)
            .WithJsonBody(new
            {
                quoteIds = ConvertToNullableInt64Enumerable(input.QuoteIds, "Quote IDs"),
                smartQuoteIds = input.SmartQuoteIds
            });

        var response = await Client.ExecuteWithErrorHandling<ProjectGroupDto>(request);
        return response;
    }


    private static void ValidateProjectGroup(ProjectGroupIdentifier projectGroupIdentifier)
    {
        if (projectGroupIdentifier is null)
            throw new PluginMisconfigurationException("Project group is required. Please check your input and try again.");

        if (string.IsNullOrWhiteSpace(projectGroupIdentifier.ProjectGroupId))
            throw new PluginMisconfigurationException("Project group ID is required. Please check your input and try again.");
    }

    private static void ValidateProjectsInput(LinkProjectsRequest input)
    {
        if (input is null)
            throw new PluginMisconfigurationException("Input is required. Please check your input and try again.");

        var hasProjectIds = input.ProjectIds?.Any() == true;
        var hasSmartProjectIds = input.SmartProjectIds?.Any() == true;

        if (!hasProjectIds && !hasSmartProjectIds)
            throw new PluginMisconfigurationException("At least one of Project IDs or Smart project IDs must be provided.");
    }

    private static void ValidateQuotesInput(LinkQuotesRequest input)
    {
        if (input is null)
            throw new PluginMisconfigurationException("Input is required. Please check your input and try again.");

        var hasQuoteIds = input.QuoteIds?.Any() == true;
        var hasSmartQuoteIds = input.SmartQuoteIds?.Any() == true;

        if (!hasQuoteIds && !hasSmartQuoteIds)
            throw new PluginMisconfigurationException("At least one of Quote IDs or Smart quote IDs must be provided.");
    }

    private IEnumerable<long>? ConvertToNullableInt64Enumerable(IEnumerable<string>? values, string fieldName)
    {
        if (values == null)
            return null;

        var list = values
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => ConvertToInt64(x, fieldName))
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .ToList();

        return list.Count != 0 ? list : null;
    }
}
