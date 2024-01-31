using System.Text.RegularExpressions;

namespace Apps.XTRF.Shared.Extensions;

public static class FilenameExtensions
{
    public static string Sanitize(this string filename)
        => Regex.Replace(filename, @"[/\\:*?""<>|]", "");
}