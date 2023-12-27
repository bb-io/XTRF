using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

/// <summary>
/// Data source handler that should be used for webhook input
/// </summary>
public class JobStatusDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "OPEN", "Open" },
        { "ACCEPTED", "Accepted" },
        { "STARTED", "Started" },
        { "READY", "Ready" },
        { "CANCELLED", "Cancelled" },
        { "OFFERS_SENT", "Offers sent" }
    };
}