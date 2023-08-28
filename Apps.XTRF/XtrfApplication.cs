using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF;

public class XtrfApplication : IApplication
{
    public string Name { get; set; }
    public XtrfApplication() { Name = "XTRF"; }

    public T GetInstance<T>()
    {
        return default;
    }
}