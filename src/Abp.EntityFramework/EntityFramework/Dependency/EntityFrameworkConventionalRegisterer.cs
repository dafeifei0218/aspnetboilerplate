using System.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Castle.MicroKernel.Registration;

namespace Abp.EntityFramework.Dependency
{
    /// <summary>
    /// Registers classes derived from AbpDbContext with configurations.
    /// EF常规注册类，从AbpDbContext（ABP数据上下文）配置继承
    /// </summary>
    public class EntityFrameworkConventionalRegisterer : IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="context">常规注册上下文</param>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<AbpDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                    .Configure(c => c.DynamicParameters(
                        (kernel, dynamicParams) =>
                        {
                            var connectionString = GetNameOrConnectionStringOrNull(context.IocManager);
                            if (!string.IsNullOrWhiteSpace(connectionString))
                            {
                                dynamicParams["nameOrConnectionString"] = connectionString;
                            }
                        })));
        }

        /// <summary>
        /// 获取链接字符串名称，或者null
        /// </summary>
        /// <param name="iocResolver">IOC控制反转解析器</param>
        /// <returns></returns>
        private static string GetNameOrConnectionStringOrNull(IIocResolver iocResolver)
        {
            if (iocResolver.IsRegistered<IAbpStartupConfiguration>())
            {
                var defaultConnectionString = iocResolver.Resolve<IAbpStartupConfiguration>().DefaultNameOrConnectionString;
                if (!string.IsNullOrWhiteSpace(defaultConnectionString))
                {
                    return defaultConnectionString;
                }
            }

            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            return null;
        }
    }
}