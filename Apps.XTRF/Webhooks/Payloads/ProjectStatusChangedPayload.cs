using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Webhooks.Payloads
{
    public class ProjectStatusChangedPayload
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("internal id")]
        [Display("Internal id")]
        public string InternalId { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("client legal name")]
        [Display("Client legal name")]
        public string ClientLegalName { get; set; }

        [JsonProperty("client price profile")]
        [Display("Client price profile")]
        public string ClientPriceProfile { get; set; }

        [JsonProperty("instructions from client")]
        [Display("Instructions from client")]
        public string InstructionsFromClient { get; set; }

        [JsonProperty("project manager")]
        [Display("Project manager")]
        public string ProjectManager { get; set; }

        [JsonProperty("created on")]
        [Display("Created on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("start date and time")]
        [Display("Start date and time")]
        public DateTime StartDateAndTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("previous status")]
        [Display("Previous status")]
        public string PreviousStatus { get; set; }
    }
}
