using System;

namespace Abp.Timing
{
    /// <summary>
    /// Defines interface for a DateTime range.
    /// 日期时间范围接口，定义日期时间范围接口。
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        /// Start time of the datetime range.
        /// 开始时间，开始的日期时间范围。
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// End time of the datetime range.
        /// 结束时间，日期范围的结束时间。
        /// </summary>
        DateTime EndTime { get; set; }
    }
}
