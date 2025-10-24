using Apps.XTRF.Smart.Actions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.SmartQuote;

public class SmartQuoteIdentifier
{
    [Display("Quote ID")]
    [DataSource(typeof(SmartQuoteCustomFieldDataHandler))]
    public string QuoteId { get; set; }
}
