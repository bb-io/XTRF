using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Webhooks.DataSourceHandlers;

public class QuoteStatusDataSourceHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "PENDING", "Pending" },
        { "SENT", "Sent" },
        { "APPROVED", "Approved" },
        { "REJECTED", "Rejected" }
    };
}