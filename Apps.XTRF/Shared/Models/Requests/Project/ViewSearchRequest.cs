using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Project
{
    public class ViewSearchRequest
    {
        [Display("View URL")]
        public string QueryUrl { get; set; }
    }
}
