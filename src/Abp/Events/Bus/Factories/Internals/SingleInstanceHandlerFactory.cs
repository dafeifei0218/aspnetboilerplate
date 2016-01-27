using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events
    /// by a single instance object. 
    /// 单实例处理程序工厂
    /// </summary>
    /// <remarks>
    /// This class always gets the same single instance of handler. 
    /// 这个类总是得到相同的处理程序的单个实例。
    /// </remarks>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// The event handler instance.
        /// 事件处理程序实例
        /// </summary>
        public IEventHandler HandlerInstance { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handler">事件处理程序</param>
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }

        /// <summary>
        /// 获取事件处理程序
        /// </summary>
        /// <returns>事件处理程序</returns>
        public IEventHandler GetHandler()
        {
            return HandlerInstance;
        }

        /// <summary>
        /// 释放事件处理程序
        /// </summary>
        /// <param name="handler">事件处理程序</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            
        }
    }
}