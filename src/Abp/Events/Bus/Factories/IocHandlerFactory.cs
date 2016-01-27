using System;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to get/release
    /// handlers using Ioc.
    /// IOC������򹤳�
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// Type of the handler.
        /// ������������
        /// </summary>
        public Type HandlerType { get; private set; }

        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// Creates a new instance of <see cref="IocHandlerFactory"/> class.
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver">Ioc���Ʒ�ת������</param>
        /// <param name="handlerType">Type of the handler ������������</param>
        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType)
        {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }

        /// <summary>
        /// Resolves handler object from Ioc container.
        /// ��ȡ�¼��������
        /// </summary>
        /// <returns>Resolved handler object �¼��������</returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        /// <summary>
        /// Releases handler object using Ioc container.
        /// �ͷ��¼��������
        /// </summary>
        /// <param name="handler">Handler to be released �¼��������</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}