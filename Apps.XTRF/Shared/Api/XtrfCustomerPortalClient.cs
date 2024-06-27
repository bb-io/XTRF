using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Utils;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.XTRF.Shared.Api;

public class XtrfCustomerPortalClient(List<AuthenticationCredentialsProvider> credentials, string token) : BlackBirdRestClient(new RestClientOptions(UrlHelper.BuildCustomerUrl(credentials.Get(CredsNames.Url).Value)))
{
    private const string TokenKey = "XTRF-CP-Auth-Token";

    public async Task<T> ExecuteRequestAsync<T>(string endpoint, Method method, object? bodyObj)
    {
        var request = new RestRequest(endpoint, method)
            .AddHeader(TokenKey, token);
        
        if (bodyObj is not null)
        {
            request.WithJsonBody(bodyObj);
        }
        
        var response = await ExecuteWithErrorHandling<T>(request);
        return response;
    }
    
    public async Task<T> UploadFileAsync<T>(string endpoint, byte[] fileBytes, string fileName)
    {
        var request = new RestRequest(endpoint, Method.Post)
            .AddHeader(TokenKey, token)
            .AddFile("file", fileBytes, fileName, "multipart/form-data");
        
        var response = await ExecuteWithErrorHandling<T>(request);
        return response;
    }
    
    public async Task<List<FileUpload>> UploadFilesAsync(IEnumerable<FileReference> files, IFileManagementClient fileManagementClient)
    {
        var fileUploadDtos = new List<FileUpload>();
        foreach (var file in files)
        {
            var stream = await fileManagementClient.DownloadAsync(file);
            var bytes = await stream.GetByteData();

            var response = await this.UploadFileAsync<List<FileUpload>>("/system/session/files", bytes, file.Name);
            fileUploadDtos.AddRange(response);
        }
        
        return fileUploadDtos;
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        return new Exception($"Error message: {response.Content}; StatusCode: {response.StatusCode}");
    }
}