namespace Apps.XTRF.Extensions;

public static class DateExtensions
{
    public static long ConvertToUnixTime(this string inputDate)
    {
        var date = DateTime.Parse(inputDate).ToUniversalTime();
        var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);

        var offset = new DateTimeOffset(unspecifiedDateKind);

        return offset.ToUnixTimeMilliseconds();
    }
}