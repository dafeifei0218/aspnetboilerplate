using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories
{
    /// <summary>
    /// Defines an interface for factories those are responsible to create/get and release of event handlers.
    /// 事件处理工厂接口
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// Gets an event handler.
        /// 获取事件处理程序
        /// </summary>
        /// <returns>The event handler 事件处理程序</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// Releases an event handler.
        /// 释放事件处理程序
        /// </summary>
        /// <param name="handler">Handle to be released 事件处理程序</param>
        void ReleaseHandler(IEventHandler handler);
    }
}