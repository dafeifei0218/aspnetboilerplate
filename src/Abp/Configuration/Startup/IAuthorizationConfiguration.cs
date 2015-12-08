using Abp.Authorization;
using Abp.Collections;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure authorization system.
    /// 授权配置接口
    /// </summary>
    public interface IAuthorizationConfiguration
    {
        /// <summary>
        /// List of authorization providers.
        /// 授权提供者列表
        /// </summary>
        ITypeList<AuthorizationProvider> Providers { get; }
    }
}