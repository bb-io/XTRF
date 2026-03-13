using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
public class ClientStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            ["ACTIVE"] = "Active",
            ["POTENTIAL"] = "Potential",
            ["INACTIVE"] = "Inactive"
        };
    }
}
