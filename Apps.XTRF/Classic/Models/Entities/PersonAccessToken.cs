using Newtonsoft.Json;

namespace Apps.XTRF.Classic.Models.Entities;

public class PersonAccessToken
{
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("token")]
    public string Token { get; set; }
}