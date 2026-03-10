using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Requests
{
    public class LinkQuotesRequest
    {
        [Display("Quote IDs")]
        public IEnumerable<string>? QuoteIds { get; set; }

        [Display("Smart quote IDs")]
        public IEnumerable<string>? SmartQuoteIds { get; set; }
    }
}
