using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
public class ClassicFileTypeDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            ["WORKFILE"] = "Workfile",
            ["TM"] = "Translation memory",
            ["DICTIONARY"] = "Dictionary",
            ["REF"] = "Reference",
            ["LOG_FILE"] = "Log file",
        };
    }
}
