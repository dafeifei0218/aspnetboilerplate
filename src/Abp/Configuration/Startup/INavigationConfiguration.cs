using Abp.Application.Navigation;
using Abp.Collections;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure navigation.
    /// 导航配置接口
    /// </summary>
    /// <remarks>
    /// NavigationManager通过具体的NavigationProvider来初始化Menus和MainMenu。
    /// 但是ABP核心模块处于最底层，怎么能知道上层定义的NavigationProvider的类型呢？ 
    /// NavigationConfiguration为解决这个问题引入了NavigationProvider配置项。
    /// NavigationProvider就是一个Type 列表 (ITypeList<NavigationProvider>),注意是NavigationProvider的Type，不是实例。
    /// 在需要NavigationProvider的地方，可以使用容器根据Type构造出实例。
    /// </remarks>
    public interface INavigationConfiguration
    {
        /// <summary>
        /// List of navigation providers.
        /// 导航提供者列表
        /// </summary>
        ITypeList<NavigationProvider> Providers { get; }
    }
}