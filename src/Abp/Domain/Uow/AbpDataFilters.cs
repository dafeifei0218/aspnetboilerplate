using Abp.Domain.Entities;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Standard filters of ABP.
    /// Abp数据过滤器
    /// </summary>
    public static class AbpDataFilters
    {
        /// <summary>
        /// "SoftDelete".
        /// Soft delete filter.
        /// Prevents getting deleted data from database.
        /// See <see cref="ISoftDelete"/> interface.
        /// 软删除
        /// </summary>
        public const string SoftDelete = "SoftDelete";

        /// <summary>
        /// "MustHaveTenant".
        /// Tenant filter to prevent getting data that is
        /// not belong to current tenant.
        /// 必须有租户
        /// 租户过滤器，以防止获得的数据不属于当前租户。
        /// </summary>
        public const string MustHaveTenant = "MustHaveTenant";

        /// <summary>
        /// "MayHaveTenant".
        /// Tenant filter to prevent getting data that is
        /// not belong to current tenant.
        /// 可能有租户
        /// 租户过滤器，以防止获得的数据不属于当前租户。
        /// </summary>
        public const string MayHaveTenant = "MayHaveTenant";

        /// <summary>
        /// Standard parameters of ABP.
        /// ABP默认参数
        /// </summary>
        public static class Parameters
        {
            /// <summary>
            /// "tenantId".
            /// 租户Id
            /// </summary>
            public const string TenantId = "tenantId";
        }
    }
}