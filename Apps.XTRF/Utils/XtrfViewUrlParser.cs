using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.XTRF.Utils
{
    public static class XtrfViewUrlParser
    {
        public static (string ViewId, string? Filters) Parse(string queryUrl)
        {
            if (string.IsNullOrWhiteSpace(queryUrl))
                throw new PluginMisconfigurationException("View URL cannot be empty.");

            if (!Uri.TryCreate(queryUrl.Trim(), UriKind.Absolute, out var uri))
                throw new PluginMisconfigurationException("View URL must be a valid absolute URL.");

            var queryParams = ParseQuery(uri.Query);

            if (!queryParams.TryGetValue("viewId", out var viewId) || string.IsNullOrWhiteSpace(viewId))
                throw new PluginMisconfigurationException("View URL must contain 'viewId' query parameter.");

            queryParams.TryGetValue("filters", out var filters);

            return (viewId.Trim(), string.IsNullOrWhiteSpace(filters) ? null : filters);
        }

        private static Dictionary<string, string> ParseQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new();

            var q = query.StartsWith("?") ? query[1..] : query;

            return q.Split('&', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Split('=', 2))
                .Where(p => p.Length >= 1)
                .ToDictionary(
                    p => Uri.UnescapeDataString(p[0]),
                    p => p.Length == 2 ? Uri.UnescapeDataString(p[1]) : string.Empty,
                    StringComparer.OrdinalIgnoreCase);
        }
    }
}
