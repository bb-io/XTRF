using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Macros;
using Apps.XTRF.Shared.Models.Requests.Provider;
using Apps.XTRF.Shared.Models.Responses.Macros;
using Apps.XTRF.Shared.Models.Responses.Provider;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System.Globalization;
using System.Text.Json;

namespace Apps.XTRF.Shared.Actions;

[ActionList("Providers")]
public class ProviderActions(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
{
    [Action("Search providers", Description = "Search for providers based on the given criteria")]
    public async Task<ProviderSearchResponse> SearchProvidersAsync([ActionParameter] ProviderSearchRequest request)
    {
        var xtrfRequest = new XtrfRequest("/providers/ids", Method.Get, Creds);
        var ids = await Client.ExecuteWithErrorHandling<List<int>>(xtrfRequest);

        var response = new ProviderSearchResponse();
        foreach (var id in ids)
        {
            var provider = await GetProviderAsync(new ProviderIdentifier
            {
                ProviderId = id.ToString()
            });

            if (request.IdNumber != null && !provider.IdNumber.Equals(request.IdNumber))
            {
                continue;
            }

            response.Providers.Add(provider);
        }

        return response;
    }

    [Action("Get provider", Description = "Get information about specific provider")]
    public async Task<ProviderResponse> GetProviderAsync([ActionParameter] ProviderIdentifier identifier)
    {
        var request = new XtrfRequest($"/providers/{identifier.ProviderId}?embed=persons", Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<ProviderResponse>(request);
    }

    [Action("Delete provider", Description = "Delete provider with the given ID")]
    public async Task DeleteProviderAsync([ActionParameter] ProviderIdentifier identifier)
    {
        var request = new XtrfRequest($"/providers/{identifier.ProviderId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Send invitation to provider", Description = "Send invitation to provider with the given ID")]
    public async Task<SendInvitationResponse> SendInvitationToProviderAsync([ActionParameter] ProviderIdentifier identifier)
    {
        var request = new XtrfRequest($"/providers/{identifier.ProviderId}/notification/invitation", Method.Post, Creds);
        return await Client.ExecuteWithErrorHandling<SendInvitationResponse>(request);
    }



    [Action("Run macros", Description = "Run a macro by ID, optionally passing a list of item IDs")]
    public async Task<RunMacroResponse> RunMacroAsync([ActionParameter] RunMacroRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.MacroId))
            throw new PluginMisconfigurationException("Macro ID is required.");

        if (!long.TryParse(request.MacroId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var macroId) || macroId <= 0)
            throw new PluginMisconfigurationException(
                $"Macro ID '{request.MacroId}' is not a valid number. XTRF expects a numeric macro id");

        var ids = new List<long>();
        if (request.Items != null)
        {
            foreach (var raw in request.Items.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                if (!long.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed) || parsed <= 0)
                {
                    throw new PluginMisconfigurationException(
                        $"Item ID '{raw}' is not a valid number. XTRF macro endpoint expects numeric IDs");
                }

                ids.Add(parsed);
            }
        }

        object? parsedParams = null;
        if (!string.IsNullOrWhiteSpace(request.Parameters))
        {
            try
            {
                var elem = JsonSerializer.Deserialize<JsonElement>(request.Parameters);

                if (elem.ValueKind != JsonValueKind.Object)
                    throw new PluginMisconfigurationException("Parameters must be a JSON object, e.g. {\"key\":\"value\"}.");

                parsedParams = elem;
            }
            catch (JsonException ex)
            {
                throw new PluginMisconfigurationException("Invalid JSON in Parameters field. Expected a JSON object.", ex);
            }
        }

        var body = new Dictionary<string, object?>
        {
            ["async"] = request.Async ?? false
        };

        if (ids.Count > 0)
            body["ids"] = ids;

        if (parsedParams != null)
            body["params"] = parsedParams;

        var xtrfRequest = new XtrfRequest($"/macros/{macroId}/run", Method.Post, Creds);
        xtrfRequest.AddJsonBody(body);

        return await Client.ExecuteWithErrorHandling<RunMacroResponse>(xtrfRequest);
    }
}