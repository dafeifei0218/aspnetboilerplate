using System;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events
    /// by a single instance object. 
    /// 瞬态事件处理程序工厂
    /// </summary>
    /// <remarks>
    /// This class always gets the same single instance of handler. 
    /// </remarks>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory 
        where THandler : IEventHandler, new()
    {
        /// <summary>
        /// Creates a new instance of the handler object.
        /// 获取事件处理程序
        /// </summary>
        /// <returns>The handler object 事件处理程序</returns>
        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        /// <summary>
        /// Disposes the handler object if it's <see cref="IDisposable"/>. Does nothing if it's not.
        /// 释放事件处理程序
        /// </summary>
        /// <param name="handler">Handler to be released 事件处理程序</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}