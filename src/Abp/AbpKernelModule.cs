using System;
using System.Reflection;
using Abp.Application.Features;
using Abp.Application.Navigation;
using Abp.Application.Services;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Interceptors;
using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Abp.Notifications;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Runtime.Validation.Interception;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;

namespace Abp
{
    /// <summary>
    /// Kernel (core) module of the ABP system.
    /// No need to depend on this, it's automatically the first module always.
    /// Abp内核模块。
    /// 内核（核心）的ABP系统模块。
    /// 不需要依赖于此，它总是自动的第一个模块。
    /// </summary>
    public sealed class AbpKernelModule : AbpModule
    {
        /// <summary>
        /// 预初始化方法
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            //验证拦截器注册
            ValidationInterceptorRegistrar.Initialize(IocManager);

            //功能拦截器注册
            FeatureInterceptorRegistrar.Initialize(IocManager);
            //审计拦截器注册
            AuditingInterceptorRegistrar.Initialize(IocManager);

            //工作单元注册
            UnitOfWorkRegistrar.Initialize(IocManager);

            //授权拦截器注册
            AuthorizationInterceptorRegistrar.Initialize(IocManager);

            //审计配置
            Configuration.Auditing.Selectors.Add(
                new NamedTypeSelector(
                    "Abp.ApplicationServices",
                    type => typeof(IApplicationService).IsAssignableFrom(type)
                    )
                );

            //本地化源配置
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    AbpConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Abp.Localization.Sources.AbpXmlSource"
                        )));

            //本地化设置
            Configuration.Settings.Providers.Add<LocalizationSettingProvider>();
            //邮件设置
            Configuration.Settings.Providers.Add<EmailSettingProvider>();
            //通知设置
            Configuration.Settings.Providers.Add<NotificationSettingProvider>();

            //工作单元注册过滤器
            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.SoftDelete, true);
            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MustHaveTenant, true);
            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MayHaveTenant, true);

            ConfigureCaches();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            //IOC容器注册事件总线
            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

            //IOC容器注册常规注册配置
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        /// <summary>
        /// 
        /// </summary>
        public override void PostInitialize()
        {
            RegisterMissingComponents();

            IocManager.Resolve<SettingDefinitionManager>().Initialize();
            IocManager.Resolve<FeatureManager>().Initialize();
            IocManager.Resolve<PermissionManager>().Initialize();
            IocManager.Resolve<LocalizationManager>().Initialize();
            IocManager.Resolve<NotificationDefinitionManager>().Initialize();
            IocManager.Resolve<NavigationManager>().Initialize();

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                var workerManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workerManager.Start();
                workerManager.Add(IocManager.Resolve<IBackgroundJobManager>());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Shutdown()
        {
            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.Resolve<IBackgroundWorkerManager>().StopAndWaitToStop();
            }
        }

        /// <summary>
        /// 配置缓存
        /// </summary>
        private void ConfigureCaches()
        {
            //应用程序设置缓存8小时
            Configuration.Caching.Configure(AbpCacheNames.ApplicationSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(8);
            });

            //租户设置缓存60分钟
            Configuration.Caching.Configure(AbpCacheNames.TenantSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(60);
            });

            //用户设置缓存20分钟
            Configuration.Caching.Configure(AbpCacheNames.UserSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(20);
            });
        }

        /// <summary>
        /// 注册缺少的组件
        /// </summary>
        private void RegisterMissingComponents()
        {
            IocManager.RegisterIfNot<IGuidGenerator, SequentialGuidGenerator>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<IAuditInfoProvider, NullAuditInfoProvider>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IAuditingStore, SimpleLogAuditingStore>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<ITenantIdResolver, NullTenantIdResolver>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IAbpSession, ClaimsAbpSession>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IRealTimeNotifier, NullRealTimeNotifier>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<INotificationStore, NullNotificationStore>(DependencyLifeStyle.Singleton);

            IocManager.RegisterIfNot<IBackgroundJobManager, BackgroundJobManager>(DependencyLifeStyle.Singleton);

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, InMemoryBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
            else
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, NullBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
        }
    }
}