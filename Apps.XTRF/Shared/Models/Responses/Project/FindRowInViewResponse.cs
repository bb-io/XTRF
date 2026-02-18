using Apps.XTRF.Shared.Models.Responses.Browser;

namespace Apps.XTRF.Shared.Models.Responses.Project
{
    public class FindRowInViewResponse
    {
        public Row? Row { get; set; }
    }

    public class SearchRowsInViewResponse
    {
        public List<Row> Rows { get; set; } = new();
        public int FilteredRows { get; set; }
        public int TotalRows { get; set; }
    }
}
