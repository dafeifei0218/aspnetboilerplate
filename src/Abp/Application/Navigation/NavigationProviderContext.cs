namespace Abp.Application.Navigation
{
    /// <summary>
    /// �����ṩ��������
    /// </summary>
    /// <remarks>
    /// �������࣬�����������Ĳ��������ֻ��װ��INavigationManager����
    /// </remarks>
    internal class NavigationProviderContext : INavigationProviderContext
    {
        /// <summary>
        /// ��������
        /// </summary>
        public INavigationManager Manager { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="manager">��������</param>
        public NavigationProviderContext(INavigationManager manager)
        {
            Manager = manager;
        }
    }
}