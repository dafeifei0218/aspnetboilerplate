using System;
using Abp.Timing;

namespace Abp.Events.Bus
{
    /// <summary>
    /// Implements <see cref="IEventData"/> and provides a base for event data classes.
    /// 事件数据
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// The time when the event occured.
        /// 事件发生的时间
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// 触发事件的对象（可选）。
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        protected EventData()
        {
            EventTime = Clock.Now;
        }
    }
}