using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.XTRF.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>() 
    {
        new ConnectionPropertyGroup
        {
            Name = "API Token",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Webhooks,
            ConnectionProperties = new List<ConnectionProperty>()
            {
                new ConnectionProperty("url") {DisplayName = "Url"},
                new ConnectionProperty("token") {DisplayName= "Token"},
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(Dictionary<string, string> values)
    {
        var url = values.First(v => v.Key == "url");
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            url.Key,
            url.Value
        );

        var token = values.First(v => v.Key == "token");
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            token.Key,
            token.Value
        );
    }
}