using Abp.Configuration.Startup;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// 后台工作配置
    /// </summary>
    internal class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        /// <summary>
        /// 是否启动工作执行
        /// </summary>
        public bool IsJobExecutionEnabled { get; set; }
        
        /// <summary>
        /// Abp启动配置
        /// </summary>
        public IAbpStartupConfiguration AbpConfiguration { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpConfiguration">Abp启动配置</param>
        public BackgroundJobConfiguration(IAbpStartupConfiguration abpConfiguration)
        {
            AbpConfiguration = abpConfiguration;

            IsJobExecutionEnabled = true;
        }
    }
}