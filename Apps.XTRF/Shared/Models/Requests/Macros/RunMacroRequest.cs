using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Macros
{
    public class RunMacroRequest
    {
        [Display("Macro ID")]
        public string MacroId { get; set; }

        [Display("Item IDs")]
        public IEnumerable<string>? Items { get; set; }

        [Display("Run asynchronously")]
        public bool? Async { get; set; }
    }
}
