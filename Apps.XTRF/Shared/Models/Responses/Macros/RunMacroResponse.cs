using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Macros
{
    public class RunMacroResponse
    {
        [Display("Action ID")]
        public string ActionId { get; set; }

        [Display("Status URL")]
        public string StatusUrl { get; set; }

        [Display("Result URL")]
        public string ResultUrl { get; set; }
    }
}
