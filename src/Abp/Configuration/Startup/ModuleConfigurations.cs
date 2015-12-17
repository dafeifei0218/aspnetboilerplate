namespace Abp.Configuration.Startup
{
    /// <summary>
    /// 模块配置
    /// </summary>
    internal class ModuleConfigurations : IModuleConfigurations
    {
        /// <summary>
        /// Abp启动配置
        /// </summary>
        public IAbpStartupConfiguration AbpConfiguration { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpConfiguration"></param>
        public ModuleConfigurations(IAbpStartupConfiguration abpConfiguration)
        {
            AbpConfiguration = abpConfiguration;
        }
    }
}