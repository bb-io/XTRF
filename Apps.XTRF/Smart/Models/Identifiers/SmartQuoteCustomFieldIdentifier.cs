using Apps.XTRF.Smart.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Identifiers;

public class SmartQuoteCustomFieldIdentifier
{
    [DataSource(typeof(SmartQuoteCustomFieldDataHandler))]
    [Display("Custom field key")]
    public string Key { get; set; }
}
