using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Models.Responses.Browser;
using Apps.XTRF.Utils;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.XTRF.Shared.Services;

internal class ViewRowsService(
    XtrfClient client,
    IEnumerable<AuthenticationCredentialsProvider> credentials)
{
    private const int PageSize = 1000;
    private readonly AuthenticationCredentialsProvider[] _credentials = credentials.ToArray();

    public async Task<ViewRowsResult> SearchAsync(string viewUrl)
    {
        var (viewId, uiFilters) = XtrfViewUrlParser.Parse(viewUrl);
        var apiFilters = XtrfUiFiltersConverter.ToApiQueryParams(uiFilters);

        var page = 1;
        var rows = new List<Row>();
        var totalRows = 0;
        var columnNames = new List<string>();

        while (true)
        {
            var request = new XtrfRequest("/browser", Method.Get, _credentials);

            request.AddQueryParameter("viewId", viewId);
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("maxRows", PageSize.ToString());

            foreach (var filter in apiFilters)
                request.AddQueryParameter(filter.Key, filter.Value);

            var result = await client.ExecuteWithErrorHandling<GetViewValuesDto>(request);

            if (page == 1)
            {
                totalRows = result.Header?.Pagination?.UnfilteredRowsCount ?? 0;
                columnNames = (result.Header?.Columns ?? Enumerable.Empty<Column>())
                    .Select(GetColumnName)
                    .ToList();
            }

            if (result.Rows?.Values?.Any() == true)
                rows.AddRange(result.Rows.Values);

            var pagination = result.Header?.Pagination;
            if (pagination == null || page >= pagination.PagesCount)
                break;

            page++;
        }

        return new ViewRowsResult
        {
            ColumnNames = columnNames,
            Rows = rows,
            TotalRows = totalRows
        };
    }

    private static string GetColumnName(Column column)
    {
        var label = !string.IsNullOrWhiteSpace(column.FullHeader)
            ? column.FullHeader
            : !string.IsNullOrWhiteSpace(column.Header)
                ? column.Header
                : column.Name;

        return string.IsNullOrWhiteSpace(column.Name) ? label : $"{label} ({column.Name})";
    }
}

internal class ViewRowsResult
{
    public List<string> ColumnNames { get; init; } = new();
    public List<Row> Rows { get; init; } = new();
    public int TotalRows { get; init; }
}
