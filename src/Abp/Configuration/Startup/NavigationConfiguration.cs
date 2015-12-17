using Abp.Application.Navigation;
using Abp.Collections;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// 导航配置
    /// </summary>
    internal class NavigationConfiguration : INavigationConfiguration
    {
        /// <summary>
        /// 导航提供者列表
        /// </summary>
        public ITypeList<NavigationProvider> Providers { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NavigationConfiguration()
        {
            Providers = new TypeList<NavigationProvider>();
        }
    }
}