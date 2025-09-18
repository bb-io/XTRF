using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Browser;

public class GetFilteredViewValuesResponse
{
    [Display("View ID")]
    public string ViewId { get; set; } = string.Empty;

    [Display("Rows")]
    public IEnumerable<Row> Rows { get; set; } = [];

    [Display("Rows matching filter")]
    public int FilteredRows { get; set; }

    [Display("Total unfiltered rows")]
    public int TotalRows { get; set; }
}
