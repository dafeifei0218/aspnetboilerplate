using Abp.MemoryDb.Configuration;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP MemoryDb module.
    /// AbpMemoryDb配置扩展类，定义扩展方法
    /// </summary>
    public static class AbpMemoryDbConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP MemoryDb module.
        /// 使用Abp内存数据库模块配置。
        /// </summary>
        public static IAbpMemoryDbModuleConfiguration AbpMemoryDb(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abp.MemoryDb", () => configurations.AbpConfiguration.IocManager.Resolve<IAbpMemoryDbModuleConfiguration>());
        }
    }
}