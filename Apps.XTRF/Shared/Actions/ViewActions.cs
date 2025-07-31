using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Requests.Browser;
using Apps.XTRF.Shared.Models.Responses.Browser;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XTRF.Shared.Actions
{
    [ActionList]
    public class ViewActions(InvocationContext invocationContext) : XtrfInvocable(invocationContext)
    {

        [Action("Get view values", Description = "Retrieve values by the ID of your view with specified columns")]
        public async Task<GetViewValuesResponse> GetViewValuesAsync([ActionParameter] GetViewValuesRequest request)
        {
            const int pageSize = 100; // valid values are 10 to 1000
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
    }
}
