//��˾�޸Ĵ��� 2016-05-26 11:40
using System;

namespace Abp.Timing
{
    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with UTC times.
    /// Utcʱ���ṩ��
    /// </summary>
    public class UtcClockProvider : IClockProvider
    {
        /// <summary>
        /// ���ڣ�
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        /// <summary>
        /// �淶����ʱ��
        /// </summary>
        /// <param name="dateTime">����ʱ��</param>
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