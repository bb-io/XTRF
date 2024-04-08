using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Classic.DataSourceHandlers.EnumHandlers;

public class ClassicFileCategoryDataHandler : IStaticDataSourceHandler
{
    private static Dictionary<string, string> EnumValues => new()
    {
        { "TM", "Translation memory" },
        { "REF", "Reference file" },
        { "WORKFILE", "Workfile" },
        { "DICTIONARY", "Dictionary" },
        { "LOG_FILE", "Log file" }
    };

    public Dictionary<string, string> GetData()
    {
        return EnumValues;
    }
}