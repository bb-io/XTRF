using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Connections
{
    public class ConnectionProvider : IConnectionProvider
    {
        public AuthenticationCredentialsProvider Create(IDictionary<string, string> connectionValues)
        {
            var credential = connectionValues.First(x => x.Key == ApiTokenKeyName);
            return new AuthenticationCredentialsProvider(AuthenticationCredentialsRequestLocation.None, ApiTokenKeyName, credential.Value);
        }

        public string ConnectionName => "Blackbird";

        private const string ApiTokenKeyName = "X-AUTH-ACCESS-TOKEN";

        public IEnumerable<string> ConnectionProperties => new[] { "url", ApiTokenKeyName };
    }
}
