using Abp.Collections;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used to configure feature system.
    /// 功能配置接口
    /// </summary>
    /// <remarks>
    /// FeatureManager通过具体的FeatureProvider来初始化FeatureDictionary（其Features属性）。
    /// 但是ABP核心模块处于项目的最底层，怎么能知道上层定义的FeatureProvider的类型呢？ 
    /// FeatureConfiguration 为解决这个问题引入了FeatureProvider配置项。
    /// FeatureProvider就是一个Type 列表 (ITypeList<FeatureProvider>),注意是FeatureProvider的Type，不是实例。
    /// 在需要FeatureProvider的地方，可以使用容器根据Type构造出实例。
    /// </remarks>
    public interface IFeatureConfiguration
    {
        /// <summary>
        /// Used to add/remove <see cref="FeatureProvider"/>s.
        /// 功能提供者，用于添加/删除
        /// </summary>
        ITypeList<FeatureProvider> Providers { get; }
    }
}