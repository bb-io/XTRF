using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Browser
{
    public class GetViewValuesResponse
    {
        [Display("View ID")]
        public string ViewId { get; set; }

        public Header Header { get; set; }

        public IEnumerable<Row> Rows { get; set; }

        public object Deferred { get; set; }
    }
}
