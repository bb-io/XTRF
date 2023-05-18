using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF
{
    public static class ExtensionMethods
    {
        public static long ConvertToUnixTime(this string inputDate)
        {
            DateTime date = DateTime.Parse(inputDate).ToUniversalTime();
            var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);

            DateTimeOffset offset = new DateTimeOffset(unspecifiedDateKind);
            long unixTime = offset.ToUnixTimeMilliseconds();

            return unixTime;
        }
    }
}
