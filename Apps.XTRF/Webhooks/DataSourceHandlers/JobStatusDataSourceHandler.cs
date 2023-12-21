using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Webhooks.DataSourceHandlers;

public class JobStatusDataSourceHandler : EnumDataHandler
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