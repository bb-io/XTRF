using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF
{
    public class XtrfApplication : IApplication
    {
        public string Name { get; set; }
        public XtrfApplication() { Name = "XTRF"; }

        public T GetInstance<T>()
        {
            return default;
        }
    }
}
