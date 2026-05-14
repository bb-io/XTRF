using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Requests.ClassicQuote
{
    public class AddQuoteReceivableRequest
    {
        [Display("Calculation unit ID")]
        public string CalculationUnitId { get; set; }

        [Display("Quantity")]
        public decimal Units { get; set; }

        [Display("Rate per unit")]
        public decimal? Rate { get; set; }

        [Display("Type")]
        public string? Type { get; set; } = "SIMPLE";

        [Display("Currency ID")]
        public string? CurrencyId { get; set; }

        [Display("Ignore minimum charge")]
        public bool? IgnoreMinimumCharge { get; set; }

        [Display("Minimum charge")]
        public decimal? MinimumCharge { get; set; }

        public string? Description { get; set; }

        [Display("Job type ID")]
        public string? JobTypeId { get; set; }

        [Display("Language combination number")]
        public string? LanguageCombinationIdNumber { get; set; }

        [Display("Source language ID")]
        [DataSource(typeof(LanguageDataHandler))]
        public string? SourceLanguageId { get; set; }

        [Display("Target language ID")]
        [DataSource(typeof(LanguageDataHandler))]
        public string? TargetLanguageId { get; set; }
    }
}
