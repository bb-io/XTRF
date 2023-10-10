using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Apps.XTRF.Constants;

public static class JsonConfig
{
    public static JsonSerializerSettings Settings => new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore
    };
}