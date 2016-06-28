using Abp.Authorization;
using Abp.Collections;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure authorization system.
    /// 授权配置接口
    /// </summary>
    /// <remarks>
    /// PermissionManager通过具体的AuthorizationProvider来初始化PermissionDictionary。
    /// 但是ABP核心模块处于最底层，怎么能知道上层定义的AuthorizationProvider的类型呢？ 
    /// AuthorizationConfiguration为解决这个问题引入了AuthorizationProvider配置项。
    /// AuthorizationProvider就是一个Type 列表 (ITypeList<AuthorizationProvider>),注意是AuthorizationProvider的Type，不是实例。
    /// 在需要AuthorizationProvider的地方，可以使用容器根据Type构造出实例。
    /// </remarks>
    public interface IAuthorizationConfiguration
    {
        /// <summary>
        /// List of authorization providers.
        /// 授权提供者列表
        /// </summary>
        ITypeList<AuthorizationProvider> Providers { get; }
    }
}