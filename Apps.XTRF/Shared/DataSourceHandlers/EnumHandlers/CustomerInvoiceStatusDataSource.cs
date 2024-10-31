using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class CustomerInvoiceStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            ["READY"] = "Ready",
            ["NOT_READY"] = "Not ready",
            ["SENT"] = "Sent",
            ["PAID"] = "Paid",
            ["CANCELED"] = "Canceled",
        };
    }
}