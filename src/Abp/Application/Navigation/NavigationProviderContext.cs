namespace Abp.Application.Navigation
{
    /// <summary>
    /// 导航提供者上下文
    /// </summary>
    /// <remarks>
    /// 上下文类，被用做方法的参数。这边只封装了INavigationManager对象。
    /// </remarks>
    internal class NavigationProviderContext : INavigationProviderContext
    {
        /// <summary>
        /// 导航管理
        /// </summary>
        public INavigationManager Manager { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="manager">导航管理</param>
        public NavigationProviderContext(INavigationManager manager)
        {
            Manager = manager;
        }
    }
}