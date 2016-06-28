using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used in <see cref="IFeatureDependency.IsSatisfiedAsync"/> method.
    /// 功能依赖上下文
    /// </summary>
    /// <remarks>
    /// 这个上下文类封装了IFeatureChecker 和 IResolver对象。被用于方法的形参。
    /// </remarks>
    public interface IFeatureDependencyContext
    {
        /// <summary>
        /// Tenant id which required the feature.
        /// Null for current tenant.
        /// 租户Id
        /// 功能的租户Id。
        /// 当前租户的为空。
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// 获取IOC控制反转解析器
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// 获取功能检查器
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IFeatureChecker FeatureChecker { get; }
    }
}