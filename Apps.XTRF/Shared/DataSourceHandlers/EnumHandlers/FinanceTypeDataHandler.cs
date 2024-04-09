using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class FinanceTypeDataHandler : IStaticDataSourceHandler
{
    private static Dictionary<string, string> EnumValues => new()
    {
        { "SIMPLE", "Simple" },
        { "CAT", "Cat" },
    };

    public Dictionary<string, string> GetData()
    {
        return EnumValues;
    }
}