using Apps.XTRF.Shared.Models.Responses.Browser;

namespace Apps.XTRF.Shared.Models.Responses.Project
{
    public class FindProjectResponse
    {
        public Row? Project { get; set; }
    }

    public class SearchProjectsResponse
    {
        public List<Row> Projects { get; set; } = new();
        public int FilteredRows { get; set; }
        public int TotalRows { get; set; }
    }
}
