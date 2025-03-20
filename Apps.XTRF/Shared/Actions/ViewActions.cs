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

                var selectedIndices = headerColumns
                .Select((col, index) => new { col, index })
                .Where(x => request.Columns.Any(r => string.Equals(r.Trim(), x.col.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                .Select(x => x.index)
                .ToList();

                result.Header.Columns = headerColumns
                .Where((col, index) => selectedIndices.Contains(index))
                .ToList();

                var rowsList = result.Rows.Values.ToList();
                foreach (var row in rowsList)
                {
                    row.Columns = selectedIndices.Select(idx => row.Columns.ElementAt(idx)).ToList();
                }
                result.Rows = rowsList
                    .Select((row, index) => new { row, index })
                    .ToDictionary(x => x.index.ToString(), x => x.row);
            }

            var response = new GetViewValuesResponse
            {
                ViewId = request.ViewId,
                Rows = result.Rows.Values
            };

            return response;
        }
    }
}
