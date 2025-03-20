using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Browser
{
    public class GetViewValuesRequest
    {
        [Display("View ID")]
        public string ViewId { get; set; }

        [Display("Columns")]
        public IEnumerable<string>? Columns { get; set; }

        [Display("Column value")]
        public IEnumerable<string>? ColumnsValue { get; set; }
    }
}
