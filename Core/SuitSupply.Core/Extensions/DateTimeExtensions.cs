using System;

namespace SuitSupply.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtcTime(this DateTime sourceDate)
        {
            var utcTime = sourceDate.ToUniversalTime();
            if ((sourceDate != utcTime) && (utcTime != DateTime.MinValue))
                sourceDate = utcTime;
            return sourceDate;
        }
    }
}