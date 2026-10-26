using System.Globalization;
using Apps.XTRF.Shared.Models.Responses.Browser;
using Apps.XTRF.Shared.Polling.Models;
using Apps.XTRF.Shared.Services;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.XTRF.Shared.Polling;

internal static class ViewPollingResponseFactory
{
    internal static PollingEventResponse<ViewRowsMemory, List<ViewRowResponse>> Create(
        ViewRowsResult view,
        ViewRowsMemory? previousMemory)
    {
        var currentRowIds = view.Rows
            .Select(GetRowId)
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        var memory = new ViewRowsMemory
        {
            RowIds = currentRowIds
        };

        if (previousMemory is null)
        {
            return new PollingEventResponse<ViewRowsMemory, List<ViewRowResponse>>
            {
                FlyBird = false,
                Memory = memory,
                Result = new List<ViewRowResponse>()
            };
        }

        var previousRowIds = previousMemory.RowIds?.ToHashSet(StringComparer.Ordinal)
            ?? new HashSet<string>(StringComparer.Ordinal);

        var newRows = view.Rows
            .Where(row => !previousRowIds.Contains(GetRowId(row)))
            .GroupBy(GetRowId, StringComparer.Ordinal)
            .Select(group => group.First())
            .Select(row => new ViewRowResponse
            {
                Columns = view.ColumnNames,
                Values = row.Columns?.ToArray() ?? Array.Empty<string>()
            })
            .ToList();

        return new PollingEventResponse<ViewRowsMemory, List<ViewRowResponse>>
        {
            FlyBird = newRows.Count > 0,
            Memory = memory,
            Result = newRows
        };
    }

    private static string GetRowId(Row row)
        => row.Id.ToString(CultureInfo.InvariantCulture);
}
