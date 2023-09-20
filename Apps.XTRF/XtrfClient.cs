using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XTRF;

public class XtrfClient : RestClient
{
    private static Uri GetUri(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var url = authenticationCredentialsProviders.First(p => p.KeyName == "url").Value;
        return new Uri(url + "/home-api");
    }

    public XtrfClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(new RestClientOptions() { ThrowOnAnyError = false, BaseUrl = GetUri(authenticationCredentialsProviders) }) { }
    
    public T ExecuteRequest<T>(XtrfRequest request)
    {
        var response = ExecuteRequest(request);
        return JsonConvert.DeserializeObject<T>(response.Content);
    }        
        
    public RestResponse ExecuteRequest(XtrfRequest request)
    { 
        var response = this.Execute(request);
        if (response.IsSuccessStatusCode)
            return response;
            
        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        throw new Exception(errorResponse.ErrorMessage);
    }    
        
    public async Task<T> ExecuteRequestAsync<T>(XtrfRequest request)
    { 
        var response = await ExecuteRequestAsync(request);
            
        return JsonConvert.DeserializeObject<T>(response.Content);
    }        
    public async Task<RestResponse> ExecuteRequestAsync(XtrfRequest request)
    { 
        var response = await ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
            return response;
        
        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        throw new Exception(errorResponse.ErrorMessage);
    }
}