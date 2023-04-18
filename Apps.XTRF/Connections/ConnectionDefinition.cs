using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Connections
{
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
            var token = values.First(x => x.Key == "token");
            yield return new AuthenticationCredentialsProvider(AuthenticationCredentialsRequestLocation.Header, "X-AUTH-ACCESS-TOKEN", token.Value);
        }
    }
}
