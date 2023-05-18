using Apps.XTRF.Responses.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF
{
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
            var response = this.Execute(request);
            if (!response.IsSuccessful)
            {
                ErrorResponse? errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                throw new Exception(errorResponse.ErrorMessage);
            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
