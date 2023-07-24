using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Apps.XTRF.Extensions;

public static class RestRequestExtensions
{
    public static RestRequest WithJsonBody(this RestRequest request, object body)
    {
        var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        });

        return request.AddJsonBody(json);
    }   
}