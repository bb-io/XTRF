using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models
{
    public class FileXTRF
    {
        [Display("ID")]
        public string Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        [Display("Category key")] public string CategoryKey { get; set; }

    }
}
