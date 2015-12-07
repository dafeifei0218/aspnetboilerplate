using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Implementation of <see cref="IFeatureDependencyContext"/>.
    /// 特征依赖上下文
    /// </summary>
    public class FeatureDependencyContext : IFeatureDependencyContext, ITransientDependency
    {
        /// <summary>
        /// 依赖注入解析器
        /// </summary>
        /// <inheritdoc/>
        public IIocResolver IocResolver { get; private set; }

        /// <summary>
        /// 功能检查
        /// </summary>
        /// <inheritdoc/>
        public IFeatureChecker FeatureChecker { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDependencyContext"/> class.
        /// 构造函数，初始化特征依赖上下文
        /// </summary>
        /// <param name="iocResolver">The ioc resolver. 依赖注入解析器</param>
        /// <param name="featureChecker">The feature checker. 功能检查</param>
        public FeatureDependencyContext(IIocResolver iocResolver, IFeatureChecker featureChecker)
        {
            IocResolver = iocResolver;
            FeatureChecker = featureChecker;
        }
    }
}