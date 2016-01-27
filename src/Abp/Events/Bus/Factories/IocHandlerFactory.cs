using System;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to get/release
    /// handlers using Ioc.
    /// IOC处理程序工厂
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// Type of the handler.
        /// 处理程序的类型
        /// </summary>
        public Type HandlerType { get; private set; }

        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// Creates a new instance of <see cref="IocHandlerFactory"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">Ioc控制反转解析器</param>
        /// <param name="handlerType">Type of the handler 处理程序的类型</param>
        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType)
        {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }

        /// <summary>
        /// Resolves handler object from Ioc container.
        /// 获取事件处理程序
        /// </summary>
        /// <returns>Resolved handler object 事件处理程序</returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        /// <summary>
        /// Releases handler object using Ioc container.
        /// 释放事件处理程序
        /// </summary>
        /// <param name="handler">Handler to be released 事件处理程序</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}