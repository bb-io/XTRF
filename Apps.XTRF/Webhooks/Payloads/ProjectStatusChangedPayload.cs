using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Payloads
{
    public class ProjectStatusChangedPayload
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

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("start date and time")]
        public DateTime StartDateAndTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("previous status")]
        public string PreviousStatus { get; set; }
    }
}
