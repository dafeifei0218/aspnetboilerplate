using System;
using Abp.Dependency;

namespace Abp.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// This event handler is an adapter to be able to use an action as <see cref="IEventHandler{TEventData}"/> implementation.
    /// �����¼�������򣬴��¼����������һ���������ö���ʵ�֡�
    /// </summary>
    /// <typeparam name="TEventData">Event type �¼�����</typeparam>
    internal class ActionEventHandler<TEventData> :
        IEventHandler<TEventData>,
        ITransientDependency
    {
        /// <summary>
        /// Action to handle the event.
        /// �������¼��������Ķ���
        /// </summary>
        public Action<TEventData> Action { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="ActionEventHandler{TEventData}"/>.
        /// ���캯��
        /// </summary>
        /// <param name="handler">Action to handle the event �¼��������Ķ���</param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// Handles the event.
        /// �¼��������
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}