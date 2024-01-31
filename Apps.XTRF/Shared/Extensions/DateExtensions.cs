using Apps.XTRF.Shared.Models.Entities;

namespace Apps.XTRF.Shared.Extensions;

public static class DateExtensions
{
    public static long ConvertToUnixTime(this DateTime inputDate, XtrfTimeZoneInfo timeZoneInfo)
    {
        var date = inputDate.ToUniversalTime();
        var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        var offset = new DateTimeOffset(unspecifiedDateKind);
        return offset.ToUnixTimeMilliseconds() + timeZoneInfo.Offset;
    }

    public static DateTime ConvertFromUnixTime(this long milliseconds, XtrfTimeZoneInfo timeZoneInfo)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds - timeZoneInfo.Offset).UtcDateTime.ToLocalTime();
}