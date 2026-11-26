using System.Text.RegularExpressions;

namespace Apps.XTRF.Shared.Extensions;

public static partial class StringExtensions
{
    private static readonly Regex InvalidChars = InvalidCharsRegex();

    public static string Sanitize(this string filename) => InvalidChars.Replace(filename, "");

    public static string ToSafeFileName(this string? name, string fallback)
    {
        var cleaned = string.IsNullOrWhiteSpace(name)
            ? string.Empty
            : InvalidChars.Replace(name, "_").Trim().TrimEnd('.');

        return string.IsNullOrWhiteSpace(cleaned) ? fallback : cleaned;
    }

    [GeneratedRegex(@"[/\\:*?""<>|\x00-\x1F]", RegexOptions.Compiled)]
    private static partial Regex InvalidCharsRegex();
}