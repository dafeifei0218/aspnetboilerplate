using Abp.NHibernate.Configuration;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP NHibernate module.
    /// Abp NHibernate配置扩展类
    /// </summary>
    public static class AbpNHibernateConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP NHibernate module.
        /// 使用Abp NHibernate模块配置
        /// </summary>
        public static IAbpNHibernateModuleConfiguration AbpNHibernate(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abp.NHibernate", () => new AbpNHibernateModuleConfiguration());
        }
    }
}