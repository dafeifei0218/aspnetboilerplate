using System;

namespace Abp.Timing
{
    /// <summary>
    /// Used to perform some common date-time operations.
    /// 时钟，用于执行一些常见的日期时间操作。
    /// </summary>
    public static class Clock
    {
        /// <summary>
        /// This object is used to perform all <see cref="Clock"/> operations.
        /// Default value: <see cref="LocalClockProvider"/>.
        /// 提供者，
        /// 该对象用于执行所有<see cref="Clock"/>操作。
        /// 默认值：<see cref="LocalClockProvider"/>.
        /// </summary>
        public static IClockProvider Provider
        {
            get { return _provider; }
            set
            {
                if (value == null)
                {
                    throw new AbpException("Can not set Clock to null!");
                }

                _provider = value;
            }
        }
        private static IClockProvider _provider;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Clock()
        {
            Provider = new LocalClockProvider();
        }

        /// <summary>
        /// Gets Now using current <see cref="Provider"/>.
        /// 获取当前时间
        /// </summary>
        public static DateTime Now
        {
            get { return Provider.Now; }
        }

        /// <summary>
        /// Normalizes given <see cref="DateTime"/> using current <see cref="Provider"/>.
        /// 规范化的时间
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized. 日期时间</param>
        /// <returns>Normalized DateTime 规范化的时间</returns>
        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}