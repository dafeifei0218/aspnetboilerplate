using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Most simple implementation of <see cref="IFeatureDependency"/>.
    /// It checks one or more features if they are enabled.
    /// 简单功能依赖注入
    /// </summary>
    public class SimpleFeatureDependency : IFeatureDependency
    {
        /// <summary>
        /// A list of features to be checked if they are enabled.
        /// 功能列表，如果启用，检查功能列表
        /// </summary>
        public string[] Features { get; set; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Features"/> must be enabled.
        /// If it's false, at least one of the <see cref="Features"/> must be enabled.
        /// Default: false.
        /// 必须全部启用，
        /// true：所有必须启用
        /// false：至少一个必须启用
        /// 默认为：false
        /// </summary>
        public bool RequiresAll { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleFeatureDependency"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="features">The features. 功能集合</param>
        public SimpleFeatureDependency(params string[] features)
        {
            Features = features;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleFeatureDependency"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="requiresAll">
        /// If this is set to true, all of the <see cref="Features"/> must be enabled.
        /// If it's false, at least one of the <see cref="Features"/> must be enabled.
        /// true：所有必须启用
        /// false：至少一个必须启用
        /// </param>
        /// <param name="features">The features. 功能集合</param>
        public SimpleFeatureDependency(bool requiresAll, params string[] features)
            : this(features)
        {
            RequiresAll = requiresAll;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="context">功能依赖注入上下文</param>
        /// <inheritdoc/>
        public Task<bool> IsSatisfiedAsync(IFeatureDependencyContext context)
        {
            return context.FeatureChecker.IsEnabledAsync(RequiresAll, Features);
        }
    }
}