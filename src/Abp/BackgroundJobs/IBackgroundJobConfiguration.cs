using Abp.Configuration.Startup;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Used to configure background job system.
    /// 后台工作配置接口，用于配置后台工作系统。
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// Used to enable/disable background job execution.
        /// 是否启动工作执行，用于启用/禁用后台作业执行。
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }

        /// <summary>
        /// Gets the ABP configuration object.
        /// 获取Abp启动配置对象。
        /// </summary>
        IAbpStartupConfiguration AbpConfiguration { get; }
    }
}