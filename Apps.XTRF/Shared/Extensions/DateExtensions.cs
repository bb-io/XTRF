namespace Apps.XTRF.Shared.Extensions;

public static class DateExtensions
{
    public static long ConvertToUnixTime(this DateTime inputDate)
    {
        var date = inputDate.ToUniversalTime();
        var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        var offset = new DateTimeOffset(unspecifiedDateKind);
        return offset.ToUnixTimeMilliseconds();
    }

    public static DateTime ConvertFromUnixTime(this long milliseconds)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime.ToLocalTime();
}