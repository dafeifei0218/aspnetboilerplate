//公司修改代码 2016-05-26 11:40
using System;

namespace Abp.Timing
{
    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with UTC times.
    /// Utc时钟提供者
    /// </summary>
    public class UtcClockProvider : IClockProvider
    {
        /// <summary>
        /// 现在，
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        /// <summary>
        /// 规范化的时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns></returns>
        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return dateTime;
        }
    }
}