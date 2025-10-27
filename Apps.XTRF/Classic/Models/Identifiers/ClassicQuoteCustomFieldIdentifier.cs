using Apps.XTRF.Classic.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Identifiers;

public class ClassicQuoteCustomFieldIdentifier
{
    [DataSource(typeof(ClassicQuoteCustomFieldDataHandler))]
    [Display("Custom field key")]
    public string Key { get; set; }
}
