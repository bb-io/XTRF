using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared;

public class XtrfApplication : IApplication
{
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