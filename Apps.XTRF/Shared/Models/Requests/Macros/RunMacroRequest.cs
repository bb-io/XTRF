using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.Macros
{
    public class RunMacroRequest
    {
        [Display("Macro ID")]
        public string MacroId { get; set; }

        [Display("Item IDs")]
        public IEnumerable<string>? Items { get; set; }

        [Display("Run asynchronously")]
        public bool? Async { get; set; }

        public string? Parameters { get; set; }
    }
}
