namespace Abp.Application.Navigation
{
    /// <summary>
    /// �����ṩ�������Ľӿ�
    /// </summary>
    internal class NavigationProviderContext : INavigationProviderContext
    {
        /// <summary>
        /// ��������
        /// </summary>
        public INavigationManager Manager { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="manager"></param>
        public NavigationProviderContext(INavigationManager manager)
        {
            Manager = manager;
        }
    }
}