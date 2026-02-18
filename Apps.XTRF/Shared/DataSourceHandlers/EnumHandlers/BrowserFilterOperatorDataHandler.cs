using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers
{
    public class BrowserFilterOperatorDataHandler : IStaticDataSourceHandler
    {
        public Dictionary<string, string> GetData()
            => new()
            {
                ["eq"] = "Equals (eq)",
                ["anyOf"] = "Any of (anyOf) - comma separated",
                ["ilike"] = "Contains (ilike)",
                ["yes"] = "Yes (boolean)",
                ["no"] = "No (boolean)",
            };
    }
}
