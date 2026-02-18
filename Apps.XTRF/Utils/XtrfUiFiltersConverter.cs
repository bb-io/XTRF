namespace Apps.XTRF.Utils
{
    public static class XtrfUiFiltersConverter
    {
        public static IEnumerable<KeyValuePair<string, string>> ToApiQueryParams(string? uiFilters)
        {
            if (string.IsNullOrWhiteSpace(uiFilters))
                return Enumerable.Empty<KeyValuePair<string, string>>();

            var parts = SplitTopLevel(uiFilters.Trim(), ';');

            var result = new List<KeyValuePair<string, string>>();

            foreach (var partRaw in parts)
            {
                var part = partRaw.Trim();
                if (string.IsNullOrWhiteSpace(part))
                    continue;

                var idx = part.IndexOf(':');
                if (idx <= 0 || idx == part.Length - 1)
                    continue;

                var field = part.Substring(0, idx).Trim();
                var expr = part.Substring(idx + 1).Trim();

                if (string.IsNullOrWhiteSpace(field) || string.IsNullOrWhiteSpace(expr))
                    continue;

                expr = NormalizeAnyOf(expr);
                var key = field.StartsWith("q.", StringComparison.OrdinalIgnoreCase)
                    ? field
                    : $"q.{field}";

                result.Add(new KeyValuePair<string, string>(key, expr));
            }

            return result;
        }

        private static string NormalizeAnyOf(string expr)
        {
            static bool IsOp(string e, string op) => e.StartsWith(op + "(", StringComparison.OrdinalIgnoreCase);

            if (!IsOp(expr, "anyOf") && !IsOp(expr, "noneOf"))
                return expr;

            var open = expr.IndexOf('(');
            var close = expr.LastIndexOf(')');
            if (open < 0 || close <= open)
                return expr;

            var opName = expr.Substring(0, open);
            var inside = expr.Substring(open + 1, close - open - 1);

            var normalized = inside.Replace(",", ";");

            return $"{opName}({normalized})";
        }

        private static IEnumerable<string> SplitTopLevel(string input, char separator)
        {
            var parts = new List<string>();
            var start = 0;
            var depth = 0;

            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (c == '(') depth++;
                else if (c == ')') depth = Math.Max(0, depth - 1);
                else if (c == separator && depth == 0)
                {
                    parts.Add(input.Substring(start, i - start));
                    start = i + 1;
                }
            }

            parts.Add(input.Substring(start));
            return parts;
        }
    }
}
