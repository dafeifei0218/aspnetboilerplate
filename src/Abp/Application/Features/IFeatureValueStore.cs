using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a store to get feature values.
    /// 功能值存储
    /// </summary>
    /// <remarks>
    /// 接口定义了获取Feature值的方法。
    /// FeatureValueStore需要在子模块中实现。
    /// 因为feature往往是存放在数据库中的。
    /// 所以Abp底层框架是不会包含对数据库有依赖的逻辑.该接口已经完全实现在了 module-zero项目中。
    /// 如果没有实现该接口，那么默认会使用NullFeatureValueStore对所有的功能返回null（此时使用默认的功能值）。
    /// </remarks>
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