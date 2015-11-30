namespace Abp.Application.Navigation
{
    /// <summary>
    /// 导航提供者上下文接口
    /// </summary>
    internal class NavigationProviderContext : INavigationProviderContext
    {
        /// <summary>
        /// 导航管理
        /// </summary>
        public INavigationManager Manager { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager"></param>
        public NavigationProviderContext(INavigationManager manager)
        {
            Manager = manager;
        }
    }
}