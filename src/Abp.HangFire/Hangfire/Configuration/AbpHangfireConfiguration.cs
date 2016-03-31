using Hangfire;
using HangfireGlobalConfiguration = Hangfire.GlobalConfiguration;

namespace Abp.Hangfire.Configuration
{
    /// <summary>
    /// Abp Hangfire��̨��������
    /// </summary>
    public class AbpHangfireConfiguration : IAbpHangfireConfiguration
    {
        /// <summary>
        /// ��̨��������
        /// </summary>
        public BackgroundJobServer Server { get; set; }

        /// <summary>
        /// ȫ������
        /// </summary>
        public IGlobalConfiguration GlobalConfiguration
        {
            get { return HangfireGlobalConfiguration.Configuration; }
        }
    }
}