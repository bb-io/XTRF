using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Shared.Webhooks.Models.Payloads;

public class CustomerPayload
{
    [JsonProperty("id")]
    [Display("ID")]
    public string Id { get; set; }

    [JsonProperty("idNumber")]
    [Display("ID number")] 
    public string IdNumber { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("fullName")]
    [Display("Full name")] 
    public string FullName { get; set; }

}