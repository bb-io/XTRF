using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;

namespace Apps.XTRF.Shared.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var client = new XtrfClient(authProviders);
        var request = new XtrfRequest("/users/me", Method.Get, authProviders);

        try
        {
            var result = await client.ExecuteWithErrorHandling(request);
            bool isJsonContent = !string.IsNullOrEmpty(result.ContentType) && 
                                 result.ContentType.Contains("json", StringComparison.OrdinalIgnoreCase);
            return new()
            {
                IsValid = isJsonContent,
                Message = isJsonContent ? string.Empty : "Invalid credentials",
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}