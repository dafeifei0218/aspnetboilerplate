using System;

namespace Abp.Timing
{
    /// <summary>
    /// Defines interface to perform some common date-time operations.
    /// 时钟提供者接口，定义接口来执行一些常见的日期操作
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// Gets Now.
        /// 获取当前时间
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Normalizes given <see cref="DateTime"/>.
        /// 规范化的时间，根据给定的日期时间规范时间
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized. 日期时间</param>
        /// <returns>Normalized DateTime 规范化的时间</returns>
        DateTime Normalize(DateTime dateTime);
    }
}