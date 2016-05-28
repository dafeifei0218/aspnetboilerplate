//��˾�޸Ĵ��� 2016-05-26 11:40
//�����޸Ĵ���δǨ�룬����˾���޸��������ļ���Ǩ�� 2016-05-26 00:33
using System;

namespace Abp.Timing
{
    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with local times.
    /// ����ʱ���ṩ��
    /// </summary>
    public class LocalClockProvider : IClockProvider
    {
        /// <summary>
        /// ��ȡ��ǰʱ��
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// �淶����ʱ��
        /// </summary>
        /// <param name="dateTime">����ʱ��</param>
        /// <returns>�淶����ʱ��</returns>
        public DateTime Normalize(DateTime dateTime)
        {
            //���ʱ���� ��δָ��Ϊ����ʱ�䣬Ҳδָ��ΪЭ��ͨ��ʱ�� (UTC)��
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            }

            //���ʱ���� Э��ͨ��ʱ�� (UTC)��
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            return dateTime;
        }
    }
}