using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Requests.Browser
{
    public class GetViewValuesRequest
    {
        [Display("View ID")]
        public string ViewId { get; set; }

        [Display("Columns")]
        [DataSource(typeof(ColumnNameDataHandler))]
        public IEnumerable<string>? Columns { get; set; }

        [Display("Column value")]
        public IEnumerable<string>? ColumnsValue { get; set; }
    }
}
