using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class ProjectStatusDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "OPEN", "Open" },
        { "CLOSED", "Closed" },
        { "CANCELLED", "Cancelled" }
    };
}