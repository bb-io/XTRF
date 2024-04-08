using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class ProjectStatusDataHandler : IStaticDataSourceHandler
{
    private static Dictionary<string, string> EnumValues => new()
    {
        { "OPEN", "Open" },
        { "CLOSED", "Closed" },
        { "CANCELLED", "Cancelled" }
    };

    public Dictionary<string, string> GetData()
    {
        return EnumValues;
    }
}