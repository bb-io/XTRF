using Newtonsoft.Json;

namespace Apps.XTRF.Classic.Models.Responses.File;

public class FileUploadedResponse
{
    [JsonProperty("token")]
    public string Token { get; set; }
}