using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses
{
    public class FileXTRF
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        [Display("Category key")] public string CategoryKey { get; set; }

    }
}
