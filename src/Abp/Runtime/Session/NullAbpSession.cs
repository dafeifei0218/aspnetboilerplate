using Abp.MultiTenancy;

namespace Abp.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IAbpSession"/>.
    /// 空Abp会话
    /// </summary>
    public class NullAbpSession : IAbpSession
    {
        /// <summary>
        /// Singleton instance.
        /// 单例实例
        /// </summary>
        public static NullAbpSession Instance { get { return SingletonInstance; } }
        private static readonly NullAbpSession SingletonInstance = new NullAbpSession();

        /// <summary>
        /// 用户Id
        /// </summary>
        /// <inheritdoc/>
        public long? UserId { get { return null; } }

        /// <summary>
        /// 租户Id
        /// </summary>
        /// <inheritdoc/>
        public int? TenantId { get { return null; } }

        /// <summary>
        /// 多租户双方
        /// </summary>
        public MultiTenancySides MultiTenancySide { get { return MultiTenancySides.Tenant; } }

        /// <summary>
        /// 模拟用户Id
        /// </summary>
        public long? ImpersonatorUserId { get { return null; } }

        /// <summary>
        /// 模拟租户Id
        /// </summary>
        public int? ImpersonatorTenantId { get { return null; } }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private NullAbpSession()
        {

        }
    }
}