using Apps.XTRF.Smart.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Identifiers;

public class SmartFileCategoryOptionalIdentifier
{
    [DataSource(typeof(SmartFileCategoryDataHandler))]
    public string? Category { get; set; }
}