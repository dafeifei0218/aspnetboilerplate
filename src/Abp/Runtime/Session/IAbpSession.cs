using Abp.MultiTenancy;

namespace Abp.Runtime.Session
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// Abp会话接口
    /// </summary>
    public interface IAbpSession
    {
        /// <summary>
        /// Gets current UserId or null.
        /// 用户Id
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// Gets current TenantId or null.
        /// 租户Id
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// Gets current multi-tenancy side.
        /// 多租户双方
        /// </summary>
        MultiTenancySides MultiTenancySide { get; }

        /// <summary>
        /// UserId of the impersonator.
        /// This is filled if a user is performing actions behalf of the <see cref="UserId"/>.
        /// 模拟用户Id
        /// </summary>
        long? ImpersonatorUserId { get; }

        /// <summary>
        /// TenantId of the impersonator.
        /// This is filled if a user with <see cref="ImpersonatorUserId"/> performing actions behalf of the <see cref="UserId"/>.
        /// 模拟租户Id
        /// </summary>
        int? ImpersonatorTenantId { get; }
    }
}
