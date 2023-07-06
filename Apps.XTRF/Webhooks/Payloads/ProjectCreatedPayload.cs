using Newtonsoft.Json;

namespace Apps.XTRF.Webhooks.Payloads
{
    public class ProjectCreatedPayload
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("internal id")]
        public string InternalId { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("client legal name")]
        public string ClientLegalName { get; set; }

        [JsonProperty("client price profile")]
        public string ClientPriceProfile { get; set; }

        [JsonProperty("instructions from client")]
        public string InstructionsFromClient { get; set; }

        [JsonProperty("project manager")]
        public string ProjectManager { get; set; }

        [JsonProperty("created on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("start date and time")]
        public DateTime StartDateAndTime { get; set; }
    }
}
