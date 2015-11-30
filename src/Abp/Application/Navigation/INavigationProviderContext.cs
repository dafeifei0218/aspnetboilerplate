namespace Abp.Application.Navigation
{
    /// <summary>
    /// Provides infrastructure to set navigation.
    /// 导航提供者上下文接口
    /// </summary>
    public interface INavigationProviderContext
    {
        /// <summary>
        /// Gets a reference to the menu manager.
        /// 导航管理
        /// </summary>
        INavigationManager Manager { get; }
    }
}