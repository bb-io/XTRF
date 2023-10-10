using Apps.XTRF.Constants;
using Apps.XTRF.Models.Responses.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XTRF.Api;

public class XtrfClient : BlackBirdRestClient
{
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
        return new Exception(errorResponse.ErrorMessage);
    }
}