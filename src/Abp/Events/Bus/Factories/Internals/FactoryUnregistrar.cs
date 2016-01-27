using System;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// Used to unregister a <see cref="IEventHandlerFactory"/> on <see cref="Dispose"/> method.
    /// 注销事件处理程序工厂
    /// </summary>
    internal class FactoryUnregistrar : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandlerFactory _factory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="eventBus">事件总线</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="factory">事件处理程序工厂</param>
        public FactoryUnregistrar(IEventBus eventBus, Type eventType, IEventHandlerFactory factory)
        {
            _eventBus = eventBus;
            _eventType = eventType;
            _factory = factory;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _eventBus.Unregister(_eventType, _factory);
        }
    }
}