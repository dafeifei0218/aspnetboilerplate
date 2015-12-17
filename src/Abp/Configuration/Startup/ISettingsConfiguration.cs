using Abp.Collections;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure setting system.
    /// 设置配置接口
    /// </summary>
    public interface ISettingsConfiguration
    {
        /// <summary>
        /// List of settings providers.
        /// 设置提供者列表
        /// </summary>
        ITypeList<SettingProvider> Providers { get; }
    }
}
