using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Requests
{
    public class LinkProjectsRequest
    {
        [Display("Project IDs")]
        public IEnumerable<string>? ProjectIds { get; set; }

        [Display("Smart project IDs")]
        public IEnumerable<string>? SmartProjectIds { get; set; }
    }
}
