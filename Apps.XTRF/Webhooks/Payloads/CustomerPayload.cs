using Newtonsoft.Json;

namespace Apps.XTRF.Webhooks.Payloads
{
    public class CustomerPayload
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

    }
}
