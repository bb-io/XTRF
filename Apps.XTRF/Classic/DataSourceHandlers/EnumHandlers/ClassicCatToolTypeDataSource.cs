using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Classic.DataSourceHandlers.EnumHandlers;

public class ClassicCatToolTypeDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            {"MEMSOURCE", "Memsource"},
            {"XTM", "XTM"},
            {"TRADOS", "Trados"},
            {"MEMOQ", "MemoQ"}
        };
    }
}