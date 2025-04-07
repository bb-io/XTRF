using Apps.XTRF.Shared.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.XTRF.Shared.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>()
    {
        new()
        {
            Name = "API Token",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>()
            {
                new(CredsNames.Url) { DisplayName = "Url" },
                new(CredsNames.ApiToken) { DisplayName = "Token", Sensitive = true },
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        yield return new AuthenticationCredentialsProvider(CredsNames.Url, values[CredsNames.Url]);
        yield return new AuthenticationCredentialsProvider(CredsNames.ApiToken, values[CredsNames.ApiToken]);
    }
}