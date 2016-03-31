using System;
using Abp.BackgroundJobs;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Hangfire.Configuration
{
    /// <summary>
    /// Abp Hangfire 后台任务配置扩展类
    /// </summary>
    public static class AbpHangfireConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP Hangfire module.
        /// Abp Hanfgire，用于配置Abp Hanfgire模块。
        /// </summary>
        public static IAbpHangfireConfiguration AbpHangfire(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abp.Hangfire", () => configurations.AbpConfiguration.IocManager.Resolve<IAbpHangfireConfiguration>());
        }

        /// <summary>
        /// Configures to use Hangfire for background job management.
        /// 配置使用Hangfire为后台作业管理。
        /// </summary>
        public static void UseHangfire(this IBackgroundJobConfiguration backgroundJobConfiguration, Action<IAbpHangfireConfiguration> configureAction)
        {
            backgroundJobConfiguration.AbpConfiguration.IocManager.RegisterIfNot<IBackgroundJobManager, HangfireBackgroundJobManager>();
            configureAction(backgroundJobConfiguration.AbpConfiguration.Modules.AbpHangfire());
        }
    }
}