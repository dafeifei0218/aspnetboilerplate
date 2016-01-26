using System;
using System.Threading.Tasks;
using Abp.Events.Bus.Factories;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus
{
    /// <summary>
    /// Defines interface of the event bus.
    /// 定义事件总线接口
    /// </summary>
    public interface IEventBus
    {
        #region Register

        /// <summary>
        /// Registers to an event.
        /// Given action is called for all event occurrences.
        /// 注册一个事件
        /// </summary>
        /// <param name="action">Action to handle events 处理事件的操作</param>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event. 
        /// Same (given) instance of the handler is used for all event occurrences.
        /// 注册一个事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="handler">Object to handle the event 事件处理程序</param>
        IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event.
        /// A new instance of <see cref="THandler"/> object is created for every event occurrence.
        /// 注册一个事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <typeparam name="THandler">Type of the event handler 事件处理程序</typeparam>
        IDisposable Register<TEventData, THandler>() where TEventData : IEventData where THandler : IEventHandler<TEventData>, new();

        /// <summary>
        /// Registers to an event.
        /// Same (given) instance of the handler is used for all event occurrences.
        /// 注册一个事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="handler">Object to handle the event 事件处理程序</param>
        IDisposable Register(Type eventType, IEventHandler handler);

        /// <summary>
        /// Registers to an event.
        /// Given factory is used to create/release handlers
        /// 注册一个事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="handlerFactory">A factory to create/release handlers 创建/释放处理程序的工厂</param>
        IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event.
        /// 注册一个事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="handlerFactory">A factory to create/release handlers 创建/释放处理程序的工厂</param>
        IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory);

        #endregion

        #region Unregister

        /// <summary>
        /// Unregisters from an event.
        /// 注销事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="action">动作</param>
        void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// Unregisters from an event.
        /// 注销事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="handler">Handler object that is registered before 注册前的处理程序对象</param>
        void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// Unregisters from an event.
        /// 注销事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="handler">Handler object that is registered before 注册前的处理程序对象</param>
        void Unregister(Type eventType, IEventHandler handler);

        /// <summary>
        /// Unregisters from an event.
        /// 注销事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="factory">Factory object that is registered before 注册前的处理程序工厂对象</param>
        void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        /// <summary>
        /// Unregisters from an event.
        /// 注销事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="factory">Factory object that is registered before 注册前的处理程序工厂对象</param>
        void Unregister(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// 注销全部事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        void UnregisterAll<TEventData>() where TEventData : IEventData;

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// 注销全部事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        void UnregisterAll(Type eventType);

        #endregion

        #region Trigger

        /// <summary>
        /// Triggers an event.
        /// 触发一个事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event.
        /// 触发一个事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="eventSource">The object which triggers the event 触发事件的对象</param>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event.
        /// 触发一个事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        void Trigger(Type eventType, IEventData eventData);

        /// <summary>
        /// Triggers an event.
        /// 触发一个事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="eventSource">The object which triggers the event 触发事件的对象</param>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        void Trigger(Type eventType, object eventSource, IEventData eventData);

        /// <summary>
        /// Triggers an event asynchronously.
        /// 异步触发一个事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        /// <returns>The task to handle async operation 处理异步操作的任务</returns>
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event asynchronously.
        /// 异步触发一个事件
        /// </summary>
        /// <typeparam name="TEventData">Event type 事件类型</typeparam>
        /// <param name="eventSource">The object which triggers the event 触发事件的对象</param>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        /// <returns>The task to handle async operation 处理异步操作的任务</returns>
        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event asynchronously.
        /// 异步触发一个事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        /// <returns>The task to handle async operation 处理异步操作的任务</returns>
        Task TriggerAsync(Type eventType, IEventData eventData);

        /// <summary>
        /// Triggers an event asynchronously.
        /// 异步触发一个事件
        /// </summary>
        /// <param name="eventType">Event type 事件类型</param>
        /// <param name="eventSource">The object which triggers the event 触发事件的对象</param>
        /// <param name="eventData">Related data for the event 事件相关数据</param>
        /// <returns>The task to handle async operation 处理异步操作的任务</returns>
        Task TriggerAsync(Type eventType, object eventSource, IEventData eventData);


        #endregion
    }
}