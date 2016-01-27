using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events
    /// by a single instance object. 
    /// ��ʵ��������򹤳�
    /// </summary>
    /// <remarks>
    /// This class always gets the same single instance of handler. 
    /// ��������ǵõ���ͬ�Ĵ������ĵ���ʵ����
    /// </remarks>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// The event handler instance.
        /// �¼��������ʵ��
        /// </summary>
        public IEventHandler HandlerInstance { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="handler">�¼��������</param>
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }

        /// <summary>
        /// ��ȡ�¼��������
        /// </summary>
        /// <returns>�¼��������</returns>
        public IEventHandler GetHandler()
        {
            return HandlerInstance;
        }

        /// <summary>
        /// �ͷ��¼��������
        /// </summary>
        /// <param name="handler">�¼��������</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            
        }
    }
}