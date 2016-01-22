using System;
using Abp.Timing;

namespace Abp.Events.Bus
{
    /// <summary>
    /// Implements <see cref="IEventData"/> and provides a base for event data classes.
    /// �¼�����
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// The time when the event occured.
        /// �¼�������ʱ��
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// �����¼��Ķ��󣨿�ѡ����
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        protected EventData()
        {
            EventTime = Clock.Now;
        }
    }
}