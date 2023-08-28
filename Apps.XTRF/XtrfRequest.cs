using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF;

public class XtrfRequest : RestRequest
{
    public XtrfRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
    {
        var token = authenticationCredentialsProviders.First(p => p.KeyName == "token").Value;
        this.AddHeader("X-AUTH-ACCESS-TOKEN", token);
        this.AddHeader("accept", "*/*");
    }
}