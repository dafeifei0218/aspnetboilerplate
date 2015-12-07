using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a store to get feature values.
    /// 功能值存储
    /// </summary>
    public interface IFeatureValueStore
    {
        /// <summary>
        /// Gets the feature value or null.
        /// 获取功能值
        /// </summary>
        /// <param name="tenantId">The tenant id. 租户Id</param>
        /// <param name="feature">The feature. 功能</param>
        Task<string> GetValueOrNullAsync(int tenantId, Feature feature);
    }
}