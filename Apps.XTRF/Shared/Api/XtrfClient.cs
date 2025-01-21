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

    protected override Exception ConfigureErrorException(RestResponse response)
    {
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