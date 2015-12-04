using Abp.Localization;
using Abp.UI.Inputs;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used in <see cref="FeatureProvider.SetFeatures"/> method as context.
    /// 特征定义上下文接口
    /// </summary>
    public interface IFeatureDefinitionContext
    {
        /// <summary>
        /// Creates a new feature.
        /// 创建一个功能
        /// </summary>
        /// <param name="name">Unique name of the feature 功能名称</param>
        /// <param name="defaultValue">Default value 默认值</param>
        /// <param name="displayName">Display name of the feature 功能显示名称</param>
        /// <param name="description">A brief description for this feature 功能描述</param>
        /// <param name="scope">Feature scope 功能范围</param>
        /// <param name="inputType">Input type 输入类型</param>
        Feature Create(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null);

        /// <summary>
        /// Gets a feature with given name or null if can not find.
        /// 
        /// </summary>
        /// <param name="name">Unique name of the feature</param>
        /// <returns><see cref="Feature"/> object or null</returns>
        Feature GetOrNull(string name);
    }
}