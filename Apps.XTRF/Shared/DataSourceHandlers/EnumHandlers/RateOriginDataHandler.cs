using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class RateOriginDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "PRICE_PROFILE", "Price profile" },
        { "PRICE_LIST", "Price list" },
        { "FILLED_MANUALLY", "Filled manually" },
        { "AUTOCALCULATED", "Autocalculated" }
    };
}