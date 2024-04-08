using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class QuoteStatusDataHandler : IStaticDataSourceHandler
{
    private static Dictionary<string, string> EnumValues => new()
    {
        { "PENDING", "Pending" },
        { "SENT", "Sent" },
        { "APPROVED", "Approved" },
        { "REJECTED", "Rejected" }
    };

    public Dictionary<string, string> GetData()
    {
        return EnumValues;
    }
}