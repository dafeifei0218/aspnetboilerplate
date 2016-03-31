using Hangfire;
using HangfireGlobalConfiguration = Hangfire.GlobalConfiguration;

namespace Abp.Hangfire.Configuration
{
    /// <summary>
    /// Abp Hangfire后台任务配置
    /// </summary>
    public class AbpHangfireConfiguration : IAbpHangfireConfiguration
    {
        /// <summary>
        /// 后台工作服务
        /// </summary>
        public BackgroundJobServer Server { get; set; }

        /// <summary>
        /// 全局配置
        /// </summary>
        public IGlobalConfiguration GlobalConfiguration
        {
            get { return HangfireGlobalConfiguration.Configuration; }
        }
    }
}