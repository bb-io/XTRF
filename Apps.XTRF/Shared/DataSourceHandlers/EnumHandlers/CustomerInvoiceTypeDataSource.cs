using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;

public class CustomerInvoiceTypeDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        { 
            { "DRAFT", "Draft" },
            { "FINAL", "Final" }
        };
    }
}