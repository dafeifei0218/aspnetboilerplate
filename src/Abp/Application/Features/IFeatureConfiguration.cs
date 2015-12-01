using Abp.Collections;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used to configure feature system.
    /// 功能配置
    /// </summary>
    public interface IFeatureConfiguration
    {
        /// <summary>
        /// Used to add/remove <see cref="FeatureProvider"/>s.
        /// 功能提供者，用于添加/删除
        /// </summary>
        ITypeList<FeatureProvider> Providers { get; }
    }
}