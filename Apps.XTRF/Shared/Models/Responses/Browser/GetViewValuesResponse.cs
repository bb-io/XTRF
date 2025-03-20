using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Shared.Models.Responses.Browser
{
    public class GetViewValuesResponse
    {
        [Display("View ID")]
        public string ViewId { get; set; }
        
        public Header Header { get; set; }

        public Dictionary<string, Row> Rows { get; set; }

        public object Deferred { get; set; }
    }
}
