using System.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Castle.MicroKernel.Registration;

namespace Abp.EntityFramework.Dependency
{
    /// <summary>
    /// Registers classes derived from AbpDbContext with configurations.
    /// 
    /// </summary>
    public class EntityFrameworkConventionalRegisterer : IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="context"></param>
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
        /// 获取链接字符串名称
        /// </summary>
        /// <param name="iocResolver"></param>
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