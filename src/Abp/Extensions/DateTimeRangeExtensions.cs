using Abp.Timing;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IDateTimeRange"/>.
    /// DateTimeRange日期时间范围扩展类
    /// </summary>
    public static class DateTimeRangeExtensions
    {
        /// <summary>
        /// Sets date range to given target.
        /// 设置日期范围为给定目标
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="target">目标</param>
        public static void SetTo(this IDateTimeRange source, IDateTimeRange target)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }

        /// <summary>
        /// Sets date range from given source.
        /// 设置日期范围从给定目标
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="source">源</param>
        public static void SetFrom(this IDateTimeRange target, IDateTimeRange source)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }
    }
}