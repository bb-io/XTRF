using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Responses.File;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XTRF.Shared.Invocables;

public class XtrfInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected XtrfClient Client { get; }

    protected XtrfInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
    }
    
    public XtrfCustomerPortalClient GetCustomerPortalClient(string token)
    {
        return new XtrfCustomerPortalClient(Creds.ToList(), token);
    }

    protected long? ConvertToInt64(string? input, string parameterName)
    {
        if (input == null)
            return null;
        
        var isSuccessful = long.TryParse(input, out var result);

        if (!isSuccessful)
            throw new PluginMisconfigurationException($"{parameterName} must be a valid number.");

        return result;
    }
    
    protected IEnumerable<long>? ConvertToInt64Enumerable(IEnumerable<string>? input, string parameterName)
    {
        var result = input?.Select(value =>
            long.TryParse(value, out var longValue)
                ? longValue
                : throw new PluginMisconfigurationException($"{parameterName} must contain valid numbers.")).ToArray();
        
        return result;
    }

    protected async Task<XtrfTimeZoneInfo> GetTimeZoneInfo()
    {
        var request = new XtrfRequest("/users/me/timeZone", Method.Get, Creds);
        var timeZoneInfo = await Client.ExecuteWithErrorHandling<XtrfTimeZoneInfo>(request);
        return timeZoneInfo;
    }
    
    protected async Task<FileUploadedResponse> UploadFile(byte[] fileBytes, string filename)
    {
        var contentType = MimeTypes.GetMimeType(filename);
        var request = new XtrfRequest("/files", Method.Post, Creds)
            .AddHeader("Content-Type", "multipart/form-data")
            .AddFile("file", fileBytes, filename, contentType);
        
        return await Client.ExecuteWithErrorHandling<FileUploadedResponse>(request);
    }

    protected async Task<PersonAccessToken> GetPersonAccessToken(string loginOrEmail)
    {
        var request = new XtrfRequest("/customers/persons/accessToken", Method.Post, Creds)
            .WithJsonBody(new { loginOrEmail });

        var response = await Client.ExecuteWithErrorHandling(request);
        return JsonConvert.DeserializeObject<PersonAccessToken>(response.Content!)!;
    }
}