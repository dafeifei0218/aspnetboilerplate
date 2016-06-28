using System.Collections.Generic;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// 导航管理类
    /// </summary>
    /// <remarks>
    /// 实现了INavigationManager，运行时是一个单例对象。完成菜单集的初始化。
    /// NavigationManager在Initialize方法中先从Configuration中获取NavigationProvider派生类的type,
    /// 然后通过容器生成该类型的实例，并调用NavigationProvider实例的SetNavigation完成菜单项的初始化。
    /// NavigationManager的Initialize方法是在AbpKernelModule的PostInitialize方法中被调用的。
    /// </remarks>
    internal class NavigationManager : INavigationManager, ISingletonDependency
    {
        /// <summary>
        /// 菜单字典
        /// </summary>
        public IDictionary<string, MenuDefinition> Menus { get; private set; }
        
        /// <summary>
        /// 主菜单
        /// </summary>
        public MenuDefinition MainMenu
        {
            get { return Menus["MainMenu"]; }
        }

        /// <summary>
        /// Ioc解析
        /// </summary>
        private readonly IIocResolver _iocResolver;
        /// <summary>
        /// 导航配置
        /// </summary>
        private readonly INavigationConfiguration _configuration;

        /// <summary>
        /// 创建一个新的<see cref="NavigationManager"/>对象
        /// </summary>
        /// <param name="iocResolver">Ioc解析</param>
        /// <param name="configuration">导航配置</param>
        public NavigationManager(IIocResolver iocResolver, INavigationConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;

            //默认添加主菜单
            Menus = new Dictionary<string, MenuDefinition>
                    {
                        {"MainMenu", new MenuDefinition("MainMenu", new FixedLocalizableString("Main menu"))} //TODO: Localization for "Main menu"
                    };
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            var context = new NavigationProviderContext(this);

            foreach (var providerType in _configuration.Providers)
            {
                var provider = (NavigationProvider)_iocResolver.Resolve(providerType);
                provider.SetNavigation(context);
            }
        }
    }
}