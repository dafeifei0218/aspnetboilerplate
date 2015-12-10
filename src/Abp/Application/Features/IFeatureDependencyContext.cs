using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used in <see cref="IFeatureDependency.IsSatisfiedAsync"/> method.
    /// 功能依赖上下文
    /// </summary>
    public interface IFeatureDependencyContext
    {
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