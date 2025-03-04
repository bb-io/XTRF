using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class ProviderInvoiceStatusDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            { "POSTPONED", "Postponed" },
            { "TO_BE_SENT", "To be sent" },
            { "SENT", "Sent" },
            { "CONFIRMED", "Confirmed" },
            { "BILL_CREATED", "Bill received"}
        };
    }
}

