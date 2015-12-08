using Abp.Authorization;
using Abp.Collections;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// 授权配置
    /// </summary>
    internal class AuthorizationConfiguration : IAuthorizationConfiguration
    {
        /// <summary>
        /// 授权提供者列表
        /// </summary>
        public ITypeList<AuthorizationProvider> Providers { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorizationConfiguration()
        {
            Providers = new TypeList<AuthorizationProvider>();
        }
    }
}