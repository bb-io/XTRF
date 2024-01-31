using Apps.XTRF.Shared.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.XTRF.Shared.Api;

public class XtrfRequest : BlackBirdRestRequest
{
    public XtrfRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> creds) 
        : base(endpoint, method, creds)
    {
    }

    protected override void AddAuth(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var token = creds.Get(CredsNames.ApiToken).Value;
        this.AddHeader("X-AUTH-ACCESS-TOKEN", token);
        this.AddHeader("accept", "*/*");
    }
}