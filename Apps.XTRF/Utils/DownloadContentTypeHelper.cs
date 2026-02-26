using System.Net.Mime;

namespace Apps.XTRF.Utils;

public static class DownloadContentTypeHelper
{
    public static string Resolve(string? responseContentType, string filename)
    {
        var normalized = Normalize(responseContentType);

        if (!IsOctetStream(normalized))
            return normalized;

        var guessed = GuessFromFilename(filename);

        return guessed ?? MediaTypeNames.Application.Octet;
    }

    private static string Normalize(string? contentType)
        => string.IsNullOrWhiteSpace(contentType)
            ? MediaTypeNames.Application.Octet
            : contentType.Split(';')[0].Trim();

    private static bool IsOctetStream(string contentType)
        => contentType.Equals(MediaTypeNames.Application.Octet, StringComparison.OrdinalIgnoreCase)
           || contentType.Equals("application/octet-stream", StringComparison.OrdinalIgnoreCase);

    private static string? GuessFromFilename(string filename)
    {
        var ext = Path.GetExtension(filename)?.ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(ext))
            return null;

        return ext switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".txt" => MediaTypeNames.Text.Plain,
            ".csv" => "text/csv",
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".zip" => "application/zip",
            ".png" => "image/png",
            ".tif" or ".tiff" => "image/tiff",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            _ => null
        };
    }
}