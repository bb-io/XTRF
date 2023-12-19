using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.DataSourceHandlers.EnumHandlers;

public class ClassicFileCategoryDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "TM", "Translation memory" },
        { "REF", "Reference file" },
        { "WORKFILE", "Workfile" },
        { "DICTIONARY", "Dictionary" },
        { "LOG_FILE", "Log file" }
    };
}