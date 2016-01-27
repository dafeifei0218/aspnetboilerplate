namespace Abp.Events.Bus.Handlers
{
    /// <summary>
    /// Defines an interface of a class that handles events of type <see cref="TEventData"/>.
    /// 事件处理程序接口
    /// </summary>
    /// <typeparam name="TEventData">Event type to handle 事件处理程序</typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        /// Handler handles the event by implementing this method.
        /// 事件处理程序
        /// </summary>
        /// <param name="eventData">Event data 事件数据</param>
        void HandleEvent(TEventData eventData);
    }
}
