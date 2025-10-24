using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XTRF.Shared.Api;

public class XtrfClient : BlackBirdRestClient
{
    protected override JsonSerializerSettings JsonSettings => JsonConfig.Settings;

    public XtrfClient(IEnumerable<AuthenticationCredentialsProvider> creds) : base(

        new()
        {
            BaseUrl = (creds.Get(CredsNames.Url).Value + "/home-api").ToUri()
        })
    {
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        var response = await ExecuteWithErrorHandling(request);
        var content = response.Content;

        if (response.ContentType?.Contains("text/html", StringComparison.OrdinalIgnoreCase) == true || content.TrimStart().StartsWith("<"))
        {
            var title = ExtractHtmlTagContent(content, "title");
            var body = ExtractHtmlTagContent(content, "body");
            var message = $"{title}: \nError Description: {body}";

            if (title.ToLower().Contains("sign in") || title.ToLower().Contains("log in"))
            {
                throw new PluginApplicationException("Failed to authenticate to the XTRF service. Please check your account permissions and try again");
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)message;
            }
            else
            {
                throw new PluginApplicationException(message);
            }
        }

        T val = JsonConvert.DeserializeObject<T>(content, JsonSettings);
        if (val == null)
        {
            throw new Exception($"Could not parse {content} to {typeof(T)}");
        }

        return val;
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        RestResponse restResponse = await ExecuteAsync(request);
        if (!restResponse.IsSuccessStatusCode)
        {
            throw ConfigureErrorException(restResponse);
        }

        return restResponse;
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (string.IsNullOrWhiteSpace(response.Content))
        {
            var message = !string.IsNullOrWhiteSpace(response.ErrorMessage)
                ? response.ErrorMessage.Trim()
                : $"Request failed with status code {response.StatusCode}";

            return new PluginApplicationException(message);
        }

        if (response.ContentType?.Contains("application/json") == true || (response.Content.TrimStart().StartsWith("{") || response.Content.TrimStart().StartsWith("[")))
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            return new PluginApplicationException(errorResponse.ErrorMessage);
        }
        else if (response.ContentType?.Contains("text/html", StringComparison.OrdinalIgnoreCase) == true || response.Content.StartsWith("<"))
        {
            var title = ExtractHtmlTagContent(response.Content, "title");
            var body = ExtractHtmlTagContent(response.Content, "body");

            var errorMessage = $"{title}: \nError Description: {body}";
            return new PluginApplicationException(errorMessage);
        }
        else
        {
            var errorMessage = $"Error: {response.ContentType}. Response Content: {response.Content}";
            throw new PluginApplicationException(errorMessage);
        }
    }


    private string ExtractHtmlTagContent(string html, string tagName)
    {
        if (string.IsNullOrEmpty(html)) return string.Empty;

        var regex = new System.Text.RegularExpressions.Regex($"<{tagName}.*?>(.*?)</{tagName}>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        var match = regex.Match(html);
        return match.Success ? match.Groups[1].Value.Trim() : "N/A";
    }
}