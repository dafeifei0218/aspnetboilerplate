namespace Abp.MultiTenancy
{
    /// <summary>
    /// Used to get current tenant id.
    /// This interface can be implemented to get Tenant's Id if user has not logged in.
    /// It can resolve TenantId from subdomain, for example.
    /// 租户Id解析器接口。
    /// 如果用户为登录，该接口可以实现以获得租户的身份证。
    /// 它可以从子域解析租户Id，例如。
    /// </summary>
    public interface ITenantIdResolver
    {
        /// <summary>
        /// Gets current TenantId or null.
        /// 获取当前租户Id或者null
        /// </summary>
        int? TenantId { get; }
    }
}