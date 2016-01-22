﻿using Abp.Application.Features;
using Abp.Auditing;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Modules;
using Abp.Reflection;
using Abp.Runtime.Caching.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Abp.Dependency.Installers
{
    /// <summary>
    /// Abp核心安装程序
    /// </summary>
    internal class AbpCoreInstaller : IWindsorInstaller
    {
        /// <summary>
        /// 安装，安装Abp的相关配置
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="store">配置存储</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
                Component.For<INavigationConfiguration, NavigationConfiguration>().ImplementedBy<NavigationConfiguration>().LifestyleSingleton(),
                Component.For<ILocalizationConfiguration, LocalizationConfiguration>().ImplementedBy<LocalizationConfiguration>().LifestyleSingleton(),
                Component.For<IAuthorizationConfiguration, AuthorizationConfiguration>().ImplementedBy<AuthorizationConfiguration>().LifestyleSingleton(),
                Component.For<IFeatureConfiguration, FeatureConfiguration>().ImplementedBy<FeatureConfiguration>().LifestyleSingleton(),
                Component.For<ISettingsConfiguration, SettingsConfiguration>().ImplementedBy<SettingsConfiguration>().LifestyleSingleton(),
                Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
                Component.For<IEventBusConfiguration, EventBusConfiguration>().ImplementedBy<EventBusConfiguration>().LifestyleSingleton(),
                Component.For<IMultiTenancyConfig, MultiTenancyConfig>().ImplementedBy<MultiTenancyConfig>().LifestyleSingleton(),
                Component.For<ICachingConfiguration, CachingConfiguration>().ImplementedBy<CachingConfiguration>().LifestyleSingleton(),
                Component.For<IAuditingConfiguration, AuditingConfiguration>().ImplementedBy<AuditingConfiguration>().LifestyleSingleton(),
                Component.For<IAbpStartupConfiguration, AbpStartupConfiguration>().ImplementedBy<AbpStartupConfiguration>().LifestyleSingleton(),
                Component.For<ITypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<IModuleFinder>().ImplementedBy<DefaultModuleFinder>().LifestyleTransient(),
                Component.For<IAbpModuleManager>().ImplementedBy<AbpModuleManager>().LifestyleSingleton(),
                Component.For<ILocalizationManager, LocalizationManager>().ImplementedBy<LocalizationManager>().LifestyleSingleton()
                );
        }
    }
}
