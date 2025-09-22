using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Browser;
using Apps.XTRF.Shared.Models.Responses.Browser;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.Actions
{
    [ActionList]
    public class ViewActions(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
    {

        [Action("Get view first matching row", Description = "Get values of the first mataching row in the specified view")]
        public async Task<GetViewValuesResponse> GetViewValuesAsync([ActionParameter] GetViewValuesRequest request)
        {
            const int pageSize = 1000; 
            int currentPage = 1;
            Dictionary<string, int> headerMapping = new();
            List<(string ColumnName, int Index, string? RequestedValue)>? requestedMapping = null;

            while (true)
            {
                var xtrfRequest = new XtrfRequest(
                    $"/browser?viewId={request.ViewId}&maxRows={pageSize}&page={currentPage}",
                    Method.Get,
                    Creds
                );

                var result = await Client.ExecuteWithErrorHandling<GetViewValuesDto>(xtrfRequest);

                if (currentPage == 1 && request.Columns != null && request.Columns.Any())
                {
                    var headerColumns = result.Header.Columns.ToList();
                    headerMapping = headerColumns
                        .Select((col, index) => new { Name = col.Name.Trim().ToLowerInvariant(), Index = index })
                        .ToDictionary(x => x.Name, x => x.Index);

                    requestedMapping = new List<(string ColumnName, int Index, string? RequestedValue)>();

                    if (request.ColumnsValue != null && request.ColumnsValue.Any() && request.Columns.Count() == request.ColumnsValue.Count())
                    {
                        foreach (var pair in request.Columns.Zip(request.ColumnsValue, (col, val) => (col, val)))
                        {
                            var normCol = pair.col.Trim().ToLowerInvariant();
                            if (headerMapping.TryGetValue(normCol, out var idx))
                            {
                                requestedMapping.Add((pair.col, idx, pair.val.Trim()));
                            }
                        }
                    }
                    else
                    {
                        foreach (var col in request.Columns)
                        {
                            var normCol = col.Trim().ToLowerInvariant();
                            if (headerMapping.TryGetValue(normCol, out var idx))
                            {
                                requestedMapping.Add((col, idx, null));
                            }
                        }
                    }
                }

                if (requestedMapping != null)
                {
                    foreach (var row in result.Rows.Values)
                    {
                        bool allMatch = true;

                        foreach (var mapping in requestedMapping)
                        {
                            if (mapping.Index >= row.Columns.Count())
                            {
                                allMatch = false;
                                break;
                            }

                            var value = row.Columns.ElementAt(mapping.Index).Trim();
                            if (mapping.RequestedValue != null &&
                                !string.Equals(value, mapping.RequestedValue, StringComparison.OrdinalIgnoreCase))
                            {
                                allMatch = false;
                                break;
                            }
                        }

                        if (allMatch)
                        {
                            return new GetViewValuesResponse
                            {
                                ViewId = request.ViewId,
                                Row = row
                            };
                        }
                    }
                }

                var pagination = result.Header.Pagination;
                if (pagination == null || currentPage >= pagination.PagesCount)
                    break;

                currentPage++;
            }

            return new GetViewValuesResponse
            {
                ViewId = request.ViewId,
                Row = null
            };
        }

        [Action("Get filtered view values", Description = "Retrieve values by the ID of your view with specified filters")]
        public async Task<GetFilteredViewValuesResponse> GetFilteredViewValuesAsync([ActionParameter] GetViewValuesRequest request)
        {
            if (request.ColumnsValue?.Count() != request.Columns?.Count())
                throw new PluginMisconfigurationException("Number of column names must match number of column filter values and both inputs are rquired for filtering.");

            const int pageSize = 1000;
            int currentPage = 1;
            var headerMapping = new Dictionary<string, int>();
            var requestedMapping = new List<(string ColumnName, int Index, string? RequestedValue)>();
            var totalRows = 0;
            var allRows = new List<Row>();

            while (true)
            {
                var xtrfRequest = new XtrfRequest(
                    $"/browser?viewId={request.ViewId}&maxRows={pageSize}&page={currentPage}",
                    Method.Get,
                    Creds
                );

                var result = await Client.ExecuteWithErrorHandling<GetViewValuesDto>(xtrfRequest);

                if (currentPage == 1 && request.Columns != null && request.Columns.Any())
                {
                    totalRows = result.Header.Pagination.UnfilteredRowsCount;
                    var headerColumns = result.Header.Columns.ToList();
                    headerMapping = headerColumns
                        .Select((col, index) => new { Name = col.Name.Trim().ToLowerInvariant(), Index = index })
                        .ToDictionary(x => x.Name, x => x.Index);

                    if (request.ColumnsValue?.Any() == true)
                    {
                        foreach (var pair in request.Columns.Zip(request.ColumnsValue, (col, val) => (col, val)))
                        {
                            var normCol = pair.col.Trim().ToLowerInvariant();
                            if (headerMapping.TryGetValue(normCol, out var idx))
                            {
                                requestedMapping.Add((pair.col, idx, pair.val.Trim()));
                            }
                        }
                    }
                }

                foreach (var row in result.Rows.Values)
                {
                    bool allMatch = true;

                    foreach (var mapping in requestedMapping)
                    {
                        if (mapping.Index >= row.Columns.Count())
                        {
                            allMatch = false;
                            break;
                        }

                        var value = row.Columns.ElementAt(mapping.Index).Trim();
                        if (mapping.RequestedValue != null &&
                            !string.Equals(value, mapping.RequestedValue, StringComparison.OrdinalIgnoreCase))
                        {
                            allMatch = false;
                            break;
                        }
                    }

                    if (allMatch)
                    {
                        allRows.Add(row);
                    }
                }

                var pagination = result.Header.Pagination;
                if (pagination == null || currentPage >= pagination.PagesCount)
                    break;

                currentPage++;
            }

            return new GetFilteredViewValuesResponse
            {
                ViewId = request.ViewId,
                Rows = allRows,
                FilteredRows = allRows.Count,
                TotalRows = totalRows
            };
        }
    }
}
