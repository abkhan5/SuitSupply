using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
