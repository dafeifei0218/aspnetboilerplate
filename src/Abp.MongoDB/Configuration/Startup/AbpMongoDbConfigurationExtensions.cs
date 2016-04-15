using Abp.MongoDb.Configuration;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP MongoDb module.
    /// AbpMongo数据库配置扩展类
    /// </summary>
    public static class AbpMongoDbConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP MongoDb module.
        /// 使用Abp MongoDb模块配置。
        /// </summary>
        public static IAbpMongoDbModuleConfiguration AbpMongoDb(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abp.MongoDb", () => configurations.AbpConfiguration.IocManager.Resolve<IAbpMongoDbModuleConfiguration>());
        }
    }
}