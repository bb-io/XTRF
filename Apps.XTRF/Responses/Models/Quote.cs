using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models
{
    public class Quote
    {
        public string Id { get; set; }
        [Display("Is classic project")] public bool IsClassicProject { get; set; }
        [Display("Quote id number")]  public string QuoteIdNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [Display("Budget code")]  public string BudgetCode { get; set; }
        [Display("Client id")] public int ClientId { get; set; }
        [Display("Service id")] public int ServiceId { get; set; }
        public string Origin { get; set; }
        [Display("Client deadline")] public long? ClientDeadline { get; set; }
        [Display("Client reference number")] public string ClientReferenceNumber { get; set; }
        [Display("Client notes")] public string ClientNotes { get; set; }
        [Display("Internal notes")] public string InternalNotes { get; set; }
        [Display("Instructions for all jobs")] public string InstructionsForAllJobs { get; set; }
        [Display("Ordered on")] public long? OrderedOn { get; set; }
    }
}
