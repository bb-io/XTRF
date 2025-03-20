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

        [Action("Get view values", Description = "Retrive values by the ID of your view with a specified columns")]
        public async Task<GetViewValuesResponse> GetViewValuesAsync([ActionParameter] GetViewValuesRequest request)
        {
            var xtrfRequest = new XtrfRequest($"/browser?viewId={request.ViewId}", Method.Get, Creds);
            var result = await Client.ExecuteWithErrorHandling<GetViewValuesDto>(xtrfRequest);


            if (request.Columns != null && request.Columns.Any())
            {
                var headerColumns = result.Header.Columns.ToList();

                var headerMapping = headerColumns
                    .Select((col, index) => new { Name = col.Name.Trim().ToLowerInvariant(), Index = index })
                    .ToDictionary(x => x.Name, x => x.Index);

                var requestedMapping = new List<(string ColumnName, int Index, string? RequestedValue)>();

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

                var filteredRows = new List<Row>();
                foreach (var row in result.Rows.Values)
                {
                    bool allMatch = true;
                    foreach (var mapping in requestedMapping)
                    {
                        var value = row.Columns.ElementAt(mapping.Index).Trim();
                        if (mapping.RequestedValue != null)
                        {
                            if (!string.Equals(value, mapping.RequestedValue, StringComparison.OrdinalIgnoreCase))
                            {
                                allMatch = false;
                                break;
                            }
                        }
                    }
                    if (allMatch)
                    {
                        filteredRows.Add(row);
                    }
                }

                result.Rows = filteredRows
                    .Select((row, index) => new { row, index })
                    .ToDictionary(x => x.index.ToString(), x => x.row);
            }

            var response = new GetViewValuesResponse
            {
                ViewId = request.ViewId,
                Row = result.Rows.Values.FirstOrDefault()
            };

            return response;
        }
    }
}
