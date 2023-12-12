namespace Apps.XTRF.Extensions;

public static class DateExtensions
{
    public static long ConvertToUnixTime(this DateTime inputDate)
    {
        var date = inputDate.ToUniversalTime();
        var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        var offset = new DateTimeOffset(unspecifiedDateKind);
        return offset.ToUnixTimeMilliseconds();
    }
}