using Apps.XTRF.Utils.Converters;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Responses.Models
{
    public class Quote
    {
        [Display("ID")] public string Id { get; set; }
        [Display("Is classic project")] public bool IsClassicProject { get; set; }
        [Display("Quote ID number")] public string QuoteIdNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [Display("Budget code")] public string BudgetCode { get; set; }

        [Display("Client ID")]
        [JsonConverter(typeof(IntToStringConverter))]
        public string ClientId { get; set; }

        [Display("Service ID")]
        [JsonConverter(typeof(IntToStringConverter))]
        public string ServiceId { get; set; }

        public string Origin { get; set; }
        [Display("Client deadline")] public long? ClientDeadline { get; set; }
        [Display("Client reference number")] public string ClientReferenceNumber { get; set; }
        [Display("Client notes")] public string ClientNotes { get; set; }
        [Display("Internal notes")] public string InternalNotes { get; set; }
        [Display("Instructions for all jobs")] public string InstructionsForAllJobs { get; set; }
        [Display("Ordered on")] public long? OrderedOn { get; set; }
    }
}