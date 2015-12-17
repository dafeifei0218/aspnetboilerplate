namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to provide a way to configure modules.
    /// Create entension methods to this class to be used over <see cref="IAbpStartupConfiguration.Modules"/> object.
    /// 模块配置接口
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the ABP configuration object.
        /// 获取模块配置
        /// </summary>
        IAbpStartupConfiguration AbpConfiguration { get; }
    }
}