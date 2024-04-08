using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class RateOriginDataHandler : IStaticDataSourceHandler
{
    private static Dictionary<string, string> EnumValues => new()
    {
        { "PRICE_PROFILE", "Price profile" },
        { "PRICE_LIST", "Price list" },
        { "FILLED_MANUALLY", "Filled manually" },
        { "AUTOCALCULATED", "Autocalculated" }
    };

    public Dictionary<string, string> GetData()
    {
        return EnumValues;
    }
}