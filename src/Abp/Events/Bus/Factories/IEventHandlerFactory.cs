using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories
{
    /// <summary>
    /// Defines an interface for factories those are responsible to create/get and release of event handlers.
    /// �¼��������ӿ�
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// Gets an event handler.
        /// ��ȡ�¼��������
        /// </summary>
        /// <returns>The event handler �¼��������</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// Releases an event handler.
        /// �ͷ��¼��������
        /// </summary>
        /// <param name="handler">Handle to be released �¼��������</param>
        void ReleaseHandler(IEventHandler handler);
    }
}