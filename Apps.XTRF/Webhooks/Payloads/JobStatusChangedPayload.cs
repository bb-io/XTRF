using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Webhooks.Payloads
{
    public class JobStatusChangedPayload
    {
        [JsonProperty("project id")]
        [Display("Project id")] 
        public string ProjectId { get; set; }

        [JsonProperty("project internal id")]
        [Display("Project internal id")]
        public string ProjectInternalId { get; set; }

        [JsonProperty("task id")]
        [Display("Task id")]
        public string TaskId { get; set; }

        [JsonProperty("task internal id")]
        [Display("Task internal id")]
        public string TaskInternalId { get; set; }

        [JsonProperty("job id")]
        [Display("Job id")]
        public string JobId { get; set; }

        [JsonProperty("job internal id")]
        [Display("Job internal id")]
        public string JobInternalId { get; set; }

        [JsonProperty("job type")]
        [Display("Job type")]
        public string JobType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("previous status")]
        [Display("Previous status")]
        public string PreviousStatus { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("actual start date and time")]
        [Display("Actual start date and time")]
        public DateTime ActualStartDateAndTime { get; set; }

        [JsonProperty("start date and time")]
        [Display("Start date and time")]
        public DateTime StartDateAndTime { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("external system in task")]
        [Display("External system in task")]
        public string ExternalSystemInTask { get; set; }

    }

}
