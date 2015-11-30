using System.Collections.Generic;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// 导航管理类
    /// </summary>
    internal class NavigationManager : INavigationManager, ISingletonDependency
    {
        public IDictionary<string, MenuDefinition> Menus { get; private set; }

        public MenuDefinition MainMenu
        {
            get { return Menus["MainMenu"]; }
        }

        private readonly IIocResolver _iocResolver;
        private readonly INavigationConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="configuration"></param>
        public NavigationManager(IIocResolver iocResolver, INavigationConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;

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