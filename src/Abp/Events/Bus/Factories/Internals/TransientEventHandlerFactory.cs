using System;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events
    /// by a single instance object. 
    /// ˲̬�¼�������򹤳�
    /// </summary>
    /// <remarks>
    /// This class always gets the same single instance of handler. 
    /// </remarks>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory 
        where THandler : IEventHandler, new()
    {
        /// <summary>
        /// Creates a new instance of the handler object.
        /// ��ȡ�¼��������
        /// </summary>
        /// <returns>The handler object �¼��������</returns>
        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        /// <summary>
        /// Disposes the handler object if it's <see cref="IDisposable"/>. Does nothing if it's not.
        /// �ͷ��¼��������
        /// </summary>
        /// <param name="handler">Handler to be released �¼��������</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}