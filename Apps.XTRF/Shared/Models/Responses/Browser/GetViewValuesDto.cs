using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Shared.Models.Responses.Browser
{
    public class GetViewValuesDto
    {
        public Header Header { get; set; }

        public Dictionary<string, Row> Rows { get; set; }

        public object Deferred { get; set; }
    }

    public class Header
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<Column> Columns { get; set; }
        public HeaderLinks Links { get; set; }
        public bool EditLinkAvailable { get; set; }
        public bool DisplayLinkAvailable { get; set; }
    }

    public class Pagination
    {
        public int RowsCount { get; set; }
        public int UnfilteredRowsCount { get; set; }
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public PaginationLinks Links { get; set; }
    }

    public class PaginationLinks
    {
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public string NextPage { get; set; }
        public string NextNextPage { get; set; }
    }

    public class Column
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public bool Sortable { get; set; }
        public bool Deferred { get; set; }
        public Dictionary<string, string> Links { get; set; }
        public string InternalHeader { get; set; }
        public string FullHeader { get; set; }
    }

    public class HeaderLinks
    {
        public string ExportToCSV { get; set; }
        public string AllIds { get; set; }
    }

    public class Row
    {
        public int Id { get; set; }
        public RowLinks Links { get; set; }
        public List<string> Columns { get; set; }
    }

    public class RowLinks
    {
        public string Edit { get; set; }
        public string Display { get; set; }
        public string GenerateDocument { get; set; }
    }
}
