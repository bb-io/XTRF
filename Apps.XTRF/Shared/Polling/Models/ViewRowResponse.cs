using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Polling.Models;

public class ViewRowResponse
{
    [Display("Columns")]
    public IEnumerable<string> Columns { get; set; } = Array.Empty<string>();

    [Display("Values")]
    public IEnumerable<string> Values { get; set; } = Array.Empty<string>();
}
