using Apps.XTRF.Shared.Models.Responses.Browser;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Project
{
    public class FindRowInViewResponse
    {
        public Row? Row { get; set; }
    }

    public class SearchRowsInViewResponse
    {

        [Display("Column names")]
        public List<string> ColumnNames { get; set; } = new();

        [Display("Rows")]
        public List<List<string>> Rows { get; set; } = new();

        public int FilteredRows { get; set; }
        public int TotalRows { get; set; }
    }
}
