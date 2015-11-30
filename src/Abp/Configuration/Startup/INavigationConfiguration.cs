using Abp.Application.Navigation;
using Abp.Collections;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure navigation.
    /// 导航配置接口
    /// </summary>
    public interface INavigationConfiguration
    {
        /// <summary>
        /// List of navigation providers.
        /// 导航提供者列表
        /// </summary>
        ITypeList<NavigationProvider> Providers { get; }
    }
}