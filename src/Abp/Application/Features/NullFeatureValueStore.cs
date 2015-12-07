using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Null pattern (default) implementation of <see cref="IFeatureValueStore"/>.
    /// It gets null for all feature values.
    /// <see cref="Instance"/> can be used via property injection of <see cref="IFeatureValueStore"/>.
    /// 空功能值存储
    /// </summary>
    public class NullFeatureValueStore : IFeatureValueStore
    {
        /// <summary>
        /// Gets the singleton instance.
        /// 获取单例实例
        /// </summary>
        public static NullFeatureValueStore Instance { get { return SingletonInstance; } }
        private static readonly NullFeatureValueStore SingletonInstance = new NullFeatureValueStore();

        /// <summary>
        ///  异步获取值或null
        /// </summary>
        /// <param name="tenantId">The tenant id. 租户Id</param>
        /// <param name="feature">The feature. 功能</param>
        /// <inheritdoc/>
        public Task<string> GetValueOrNullAsync(int tenantId, Feature feature)
        {
            return Task.FromResult((string) null);
        }
    }
}