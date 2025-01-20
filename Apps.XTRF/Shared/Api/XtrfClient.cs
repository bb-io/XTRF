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
        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        return new PluginApplicationException(errorResponse.ErrorMessage);
    }
}