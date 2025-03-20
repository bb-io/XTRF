using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Browser
{
    public class GetViewValuesResponse
    {
        [Display("View ID")]
        public string ViewId { get; set; }

        [Display("Raws value")]
        public IEnumerable<Row> Rows { get; set; }
    }
}
