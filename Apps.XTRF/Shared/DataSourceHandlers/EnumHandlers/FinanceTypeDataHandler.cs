using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class FinanceTypeDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "SIMPLE", "Simple" },
        { "CAT", "Cat" },
    };
}