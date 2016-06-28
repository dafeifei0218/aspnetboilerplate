using Abp.Localization;
using Abp.UI.Inputs;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used in <see cref="FeatureProvider.SetFeatures"/> method as context.
    /// �������������Ľӿ�
    /// </summary>
    /// <remarks>
    /// IFeatureDefinitionContext/FeatureDefinitionContextBase : 
    /// ����ӿںͳ������ṩ����FeatureDictionary�ķ���, �½�һ��Feature��FeatureDictionary��,�͸��� Name�� FeatureDictionary����һ��Featur��
    /// ������FeatureManager�Ļ��ࡣ
    /// </remarks>
    public interface IFeatureDefinitionContext
    {
        /// <summary>
        /// Creates a new feature.
        /// ����һ������
        /// </summary>
        /// <param name="name">Unique name of the feature ��������</param>
        /// <param name="defaultValue">Default value Ĭ��ֵ</param>
        /// <param name="displayName">Display name of the feature ������ʾ����</param>
        /// <param name="description">A brief description for this feature ��������</param>
        /// <param name="scope">Feature scope ���ܷ�Χ</param>
        /// <param name="inputType">Input type ��������</param>
        Feature Create(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null);

        /// <summary>
        /// Gets a feature with given name or null if can not find.
        /// ��ȡһ�����ܣ����δ�Ҳ�������null
        /// </summary>
        /// <param name="name">Unique name of the feature ��������</param>
        /// <returns><see cref="Feature"/> object or null ���ܶ����null</returns>
        Feature GetOrNull(string name);
    }
}