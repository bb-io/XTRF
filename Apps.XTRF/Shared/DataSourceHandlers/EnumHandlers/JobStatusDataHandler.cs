using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

/// <summary>
/// Data source handler that should be used for webhook input
/// </summary>
public class StaticJobStatusDataHandler : IStaticDataSourceHandler
{
    private static Dictionary<string, string> EnumValues => new()
    {
        { "OPEN", "Open" },
        { "ACCEPTED", "Accepted" },
        { "STARTED", "Started" },
        { "READY", "Ready" },
        { "CANCELLED", "Cancelled" },
        { "OFFERS_SENT", "Offers sent" }
    };

    public Dictionary<string, string> GetData()
    {
        return EnumValues;
    }
}