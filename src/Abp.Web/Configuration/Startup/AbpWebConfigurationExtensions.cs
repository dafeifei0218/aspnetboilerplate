using Abp.Web.Configuration;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP Web module.
    /// Abp Web配置扩展类
    /// </summary>
    public static class AbpWebConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP Web module.
        /// 使用ABP Web模块配置
        /// </summary>
        public static IAbpWebModuleConfiguration AbpWeb(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abp.Web", () => configurations.AbpConfiguration.IocManager.Resolve<IAbpWebModuleConfiguration>());
        }
    }
}