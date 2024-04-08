using Apps.XTRF.Smart.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Identifiers;

public class SmartFileCategoryIdentifier
{
    [StaticDataSource(typeof(SmartFileCategoryDataHandler))]
    public string Category { get; set; }
}

public class SmartFileCategoryOptionalIdentifier
{
    [StaticDataSource(typeof(SmartFileCategoryDataHandler))]
    public string? Category { get; set; }
}