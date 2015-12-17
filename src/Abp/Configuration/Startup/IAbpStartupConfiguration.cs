using Abp.Application.Features;
using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Runtime.Caching.Configuration;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure ABP and modules on startup.
    /// Abp启动配置
    /// </summary>
    public interface IAbpStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// Gets the IOC manager associated with this configuration.
        /// 管理类
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// Used to set localization configuration.
        /// 本地化配置
        /// </summary>
        ILocalizationConfiguration Localization { get; }

        /// <summary>
        /// Used to configure navigation.
        /// 
        /// </summary>
        INavigationConfiguration Navigation { get; }

        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// 
        /// </summary>
        IEventBusConfiguration EventBus { get; }

        /// <summary>
        /// Used to configure auditing.
        /// 
        /// </summary>
        IAuditingConfiguration Auditing { get; }

        /// <summary>
        /// Used to configure caching.
        /// 
        /// </summary>
        ICachingConfiguration Caching { get; }

        /// <summary>
        /// Used to configure multi-tenancy.
        /// 
        /// </summary>
        IMultiTenancyConfig MultiTenancy { get; }

        /// <summary>
        /// Used to configure authorization.
        /// 授权配置
        /// </summary>
        IAuthorizationConfiguration Authorization { get; }

        /// <summary>
        /// Used to configure settings.
        /// 设置配置
        /// </summary>
        ISettingsConfiguration Settings { get; }

        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// 默认链接字符串
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// Used to configure modules.
        /// Modules can write extension methods to <see cref="IModuleConfigurations"/> to add module specific configurations.
        /// 模块配置
        /// </summary>
        IModuleConfigurations Modules { get; }

        /// <summary>
        /// Used to configure unit of work defaults.
        /// 工作段元默认选项
        /// </summary>
        IUnitOfWorkDefaultOptions UnitOfWork { get; }

        /// <summary>
        /// Used to configure features.
        /// 特征配置
        /// </summary>
        IFeatureConfiguration Features { get; }
    }
}