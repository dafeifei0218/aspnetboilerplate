namespace Abp.MultiTenancy
{
    /// <summary>
    /// Implements null object pattern for <see cref="ITenantIdResolver"/>.
    /// 空租户Id解析器
    /// </summary>
    public class NullTenantIdResolver : ITenantIdResolver
    {
        /// <summary>
        /// Singleton instance.
        /// 单例实例
        /// </summary>
        public static NullTenantIdResolver Instance { get { return SingletonInstance; } }
        private static readonly NullTenantIdResolver SingletonInstance = new NullTenantIdResolver();

        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get { return null; } }
    }
}