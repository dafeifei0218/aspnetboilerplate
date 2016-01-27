using System;
using Abp.Dependency;

namespace Abp.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// This event handler is an adapter to be able to use an action as <see cref="IEventHandler{TEventData}"/> implementation.
    /// 动作事件处理程序，此事件处理程序是一个适配器用动作实现。
    /// </summary>
    /// <typeparam name="TEventData">Event type 事件类型</typeparam>
    internal class ActionEventHandler<TEventData> :
        IEventHandler<TEventData>,
        ITransientDependency
    {
        /// <summary>
        /// Action to handle the event.
        /// 动作，事件处理程序的动作
        /// </summary>
        public Action<TEventData> Action { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="ActionEventHandler{TEventData}"/>.
        /// 构造函数
        /// </summary>
        /// <param name="handler">Action to handle the event 事件处理程序的动作</param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// Handles the event.
        /// 事件处理程序
        /// </summary>
        /// <param name="eventData">事件数据</param>
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}