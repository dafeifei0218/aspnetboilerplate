using Abp.Application.Features;
using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Runtime.Caching.Configuration;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// This class is used to configure ABP and modules on startup.
    /// Abp启动配置
    /// </summary>
    internal class AbpStartupConfiguration : DictionayBasedConfig, IAbpStartupConfiguration
    {
        /// <summary>
        /// IOC管理类
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Used to set localization configuration.
        /// 本地化配置
        /// </summary>
        public ILocalizationConfiguration Localization { get; private set; }

        /// <summary>
        /// Used to configure authorization.
        /// 授权配置
        /// </summary>
        public IAuthorizationConfiguration Authorization { get; private set; }

        /// <summary>
        /// Used to configure settings.
        /// 设置配置
        /// </summary>
        public ISettingsConfiguration Settings { get; private set; }

        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// 默认链接字符串
        /// </summary>
        public string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// Used to configure modules.
        /// Modules can write extension methods to <see cref="ModuleConfigurations"/> to add module specific configurations.
        /// 模块配置
        /// </summary>
        public IModuleConfigurations Modules { get; private set; }

        /// <summary>
        /// Used to configure unit of work defaults.
        /// 工作单元默认选项
        /// </summary>
        public IUnitOfWorkDefaultOptions UnitOfWork { get; private set; }

        /// <summary>
        /// Used to configure features.
        /// 特征配置
        /// </summary>
        public IFeatureConfiguration Features { get; private set; }

        /// <summary>
        /// Used to configure navigation.
        /// 导航配置
        /// </summary>
        public INavigationConfiguration Navigation { get; private set; }

        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// 事件总线配置
        /// </summary>
        public IEventBusConfiguration EventBus { get; private set; }

        /// <summary>
        /// Used to configure auditing.
        /// 授权配置
        /// </summary>
        public IAuditingConfiguration Auditing { get; private set; }

        /// <summary>
        /// 缓存配置
        /// </summary>
        public ICachingConfiguration Caching { get; private set; }

        /// <summary>
        /// Used to configure multi-tenancy.
        /// 多租户配置
        /// </summary>
        public IMultiTenancyConfig MultiTenancy { get; private set; }

        /// <summary>
        /// Private constructor for singleton pattern.
        /// 构造函数
        /// </summary>
        public AbpStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            Localization = IocManager.Resolve<ILocalizationConfiguration>();
            Modules = IocManager.Resolve<IModuleConfigurations>();
            Features = IocManager.Resolve<IFeatureConfiguration>();
            Navigation = IocManager.Resolve<INavigationConfiguration>();
            Authorization = IocManager.Resolve<IAuthorizationConfiguration>();
            Settings = IocManager.Resolve<ISettingsConfiguration>();
            UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            EventBus = IocManager.Resolve<IEventBusConfiguration>();
            MultiTenancy = IocManager.Resolve<IMultiTenancyConfig>();
            Auditing = IocManager.Resolve<IAuditingConfiguration>();
            Caching = IocManager.Resolve<ICachingConfiguration>();
        }
    }
}