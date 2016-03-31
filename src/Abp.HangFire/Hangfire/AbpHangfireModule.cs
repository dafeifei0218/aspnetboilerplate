using System.Reflection;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Hangfire;

namespace Abp.Hangfire
{
    /// <summary>
    /// Abp Hangfire模块
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpHangfireModule : AbpModule
    {
        /// <summary>
        /// 预初始化
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.Register<IAbpHangfireConfiguration, AbpHangfireConfiguration>();
            
            Configuration.Modules
                .AbpHangfire()
                .GlobalConfiguration
                .UseActivator(new HangfireIocJobActivator(IocManager));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
