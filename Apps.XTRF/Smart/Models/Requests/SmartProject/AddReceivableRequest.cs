using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.SmartProject
{
    public class AddReceivableRequest
    {
        [Display("Receivable ID")]
        public string? Id { get; set; }

        [Display("Job type ID")]
        public string JobTypeId { get; set; }

        [Display("Source language ID")]
        [DataSource(typeof(LanguageDataHandler))]
        public string SourceLanguageId { get; set; }

        [Display("Target language ID")]
        [DataSource(typeof(LanguageDataHandler))]
        public string TargetLanguageId { get; set; }

        [Display("Rate origin")]
        public string? RateOrigin { get; set; }

        [Display("Currency ID")]
        public string CurrencyId { get; set; }

        [Display("Total")]
        public string Total { get; set; }

        [Display("Invoice ID")]
        public string? InvoiceId { get; set; }

        [Display("Type")]
        public string? Type { get; set; }

        [Display("Calculation unit ID")]
        public string CalculationUnitId { get; set; }

        [Display("Ignore minimum charge")]
        public bool? IgnoreMinimumCharge { get; set; }

        [Display("Minimum charge")]
        public decimal? MinimumCharge { get; set; }

        [Display("Description")]
        public string? Description { get; set; }

        [Display("Rate")]
        public decimal? Rate { get; set; }

        [Display("Quantity")]
        public decimal? Quantity { get; set; }
    }
}
