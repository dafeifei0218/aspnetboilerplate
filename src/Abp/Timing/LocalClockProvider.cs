//公司修改代码 2016-05-26 11:40
//家中修改代码未迁入，到公司再修改这两个文件再迁入 2016-05-26 00:33
using System;

namespace Abp.Timing
{
    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with local times.
    /// 本地时钟提供者
    /// </summary>
    public class LocalClockProvider : IClockProvider
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// 规范化的时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>规范化的时间</returns>
        public DateTime Normalize(DateTime dateTime)
        {
            //如果时间是 既未指定为本地时间，也未指定为协调通用时间 (UTC)。
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            }

            //如果时间是 协调通用时间 (UTC)。
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            return dateTime;
        }
    }
}