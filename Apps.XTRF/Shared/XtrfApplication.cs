using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.XTRF.Shared;

public class XtrfApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.TranslationBusinessManagement];
        set { }
    }
    
    public string Name
    {
        get => "XTRF";
        set { }
    }

    public T GetInstance<T>()
    {
        return default;
    }
}