using System;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="EventHandler"/>.
    /// EventHandler事件处理程序扩展类
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Raises given event safely with given arguments.
        /// 安全地调用，用给定的参数安全地调用事件处理程序
        /// </summary>
        /// <param name="eventHandler">The event handler 事件处理程序</param>
        /// <param name="sender">Source of the event 事件来源</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Raises given event safely with given arguments.
        /// 安全地调用，用给定的参数安全地调用事件处理程序
        /// </summary>
        /// <param name="eventHandler">The event handler 事件处理程序</param>
        /// <param name="sender">Source of the event 事件来源</param>
        /// <param name="e">Event argument 事件参数</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }

        /// <summary>
        /// Raises given event safely with given arguments.
        ///  安全地调用，用给定的参数安全地调用事件处理程序
        /// </summary>
        /// <typeparam name="TEventArgs">Type of the <see cref="EventArgs"/> 事件参数</typeparam>
        /// <param name="eventHandler">The event handler 事件处理程序</param>
        /// <param name="sender">Source of the event 事件来源</param>
        /// <param name="e">Event argument 事件参数</param>
        public static void InvokeSafely<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }
    }
}