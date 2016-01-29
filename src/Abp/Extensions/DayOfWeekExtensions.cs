using System;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DayOfWeekExtensions"/>.
    /// DayOfWeek指定一周的某天扩展类
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// Check if given <see cref="DayOfWeek"/> value is weekend.
        /// 检查值是否是周末
        /// </summary>
        /// <param name="dayOfWeek">指定一周的某天</param>
        public static bool IsWeekend(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// Check if given <see cref="DayOfWeek"/> value is weekday.
        /// 是否是工作日，检查值是否是工作日
        /// </summary>
        /// <param name="dayOfWeek">指定一周的某天</param>
        public static bool IsWeekday(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }
    }
}