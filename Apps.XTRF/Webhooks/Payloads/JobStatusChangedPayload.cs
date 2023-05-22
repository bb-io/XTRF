using Apps.XTRF.Responses.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Payloads
{
    public class JobStatusChangedPayload
    {
        [JsonProperty("project id")]
        public string ProjectId { get; set; }

        [JsonProperty("project internal id")]
        public string ProjectInternalId { get; set; }

        [JsonProperty("quote id")]
        public string QuoteId { get; set; }

        [JsonProperty("quote internal id")]
        public string QuoteInternalId { get; set; }

        [JsonProperty("task id")]
        public string TaskId { get; set; }

        [JsonProperty("task internal id")]
        public string TaskInternalId { get; set; }

        [JsonProperty("job id")]
        public string JobId { get; set; }

        [JsonProperty("job internal id")]
        public string JobInternalId { get; set; }

        [JsonProperty("job type")]
        public string JobType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("previous status")]
        public string PreviousStatus { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("assignee")]
        public string Assignee { get; set; }

        [JsonProperty("actual start date and time")]
        public DateTime ActualStartDateAndTime { get; set; }

        [JsonProperty("start date and time")]
        public DateTime StartDateAndTime { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("external system in task")]
        public string ExternalSystemInTask { get; set; }

    }

}
