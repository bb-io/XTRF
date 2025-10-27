using Apps.XTRF.Smart.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.SmartQuote;

public class SmartCustomFieldIdentifier
{
    [DataSource(typeof(SmartQuoteCustomFieldDataHandler))]
    [Display("Custom field key")]
    public string Key { get; set; }
}
