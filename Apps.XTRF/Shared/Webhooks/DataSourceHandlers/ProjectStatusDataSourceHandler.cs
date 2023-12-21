using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Shared.Webhooks.DataSourceHandlers;

public class ProjectStatusDataSourceHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "OPEN", "Open" },
        { "CLOSED", "Closed" },
        { "CANCELLED", "Cancelled" }
    };
}