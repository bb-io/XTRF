using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Browser
{
    public class GetViewValuesResponse
    {
        [Display("View ID")]
        public string ViewId { get; set; }

        [Display("Raw value")]
        public Row Row { get; set; }
    }
}
