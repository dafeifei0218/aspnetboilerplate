using System;

namespace Abp.Timing
{
    /// <summary>
    /// Used to perform some common date-time operations.
    /// ʱ�ӣ�����ִ��һЩ����������ʱ�������
    /// </summary>
    public static class Clock
    {
        /// <summary>
        /// This object is used to perform all <see cref="Clock"/> operations.
        /// Default value: <see cref="LocalClockProvider"/>.
        /// �ṩ�ߣ�
        /// �ö�������ִ������<see cref="Clock"/>������
        /// Ĭ��ֵ��<see cref="LocalClockProvider"/>.
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
        /// ��̬���캯��
        /// </summary>
        static Clock()
        {
            Provider = new LocalClockProvider();
        }

        /// <summary>
        /// Gets Now using current <see cref="Provider"/>.
        /// ��ȡ��ǰʱ��
        /// </summary>
        public static DateTime Now
        {
            get { return Provider.Now; }
        }

        /// <summary>
        /// Normalizes given <see cref="DateTime"/> using current <see cref="Provider"/>.
        /// �淶����ʱ��
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized. ����ʱ��</param>
        /// <returns>Normalized DateTime �淶����ʱ��</returns>
        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}