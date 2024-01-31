using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class QuoteStatusDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "PENDING", "Pending" },
        { "SENT", "Sent" },
        { "APPROVED", "Approved" },
        { "REJECTED", "Rejected" }
    };
}