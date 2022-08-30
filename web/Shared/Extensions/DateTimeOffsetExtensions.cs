using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Web.Shared.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset TruncateToYearStart(this DateTimeOffset dt)
        {
            return new DateTimeOffset(dt.Year, 1, 1, 0, 0 ,0 , 0, dt.Offset);
        }

        public static DateTimeOffset TruncateToMonthStart(this DateTimeOffset dt)
        {
            return new DateTimeOffset(dt.Year, dt.Month, 1, 0, 0, 0, dt.Offset);
        }

        public static DateTimeOffset TruncateToDayStart(this DateTimeOffset dt)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, dt.Offset);
        }

        public static DateTimeOffset TruncateToHourStart(this DateTimeOffset dt)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, dt.Offset);
        }

        public static DateTimeOffset TruncateToMinuteStart(this DateTimeOffset dt)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, dt.Offset);
        }

        public static DateTimeOffset TruncateToSecondStart(this DateTimeOffset dt)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Offset);
        }
    }
}
