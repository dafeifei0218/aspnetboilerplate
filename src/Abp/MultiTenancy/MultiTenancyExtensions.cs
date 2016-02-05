using Abp.Domain.Entities;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// Extension methods for multi-tenancy.
    /// 多租户扩展类
    /// </summary>
    public static class MultiTenancyExtensions
    {
        /// <summary>
        /// Gets multi-tenancy side (<see cref="MultiTenancySides"/>) of an object that implements <see cref="IMayHaveTenant"/>.
        /// 获取多租户
        /// </summary>
        /// <param name="obj">The object 多租户对象</param>
        public static MultiTenancySides GetMultiTenancySide(this IMayHaveTenant obj)
        {
            return obj.TenantId.HasValue
                ? MultiTenancySides.Tenant
                : MultiTenancySides.Host;
        }
    }
}