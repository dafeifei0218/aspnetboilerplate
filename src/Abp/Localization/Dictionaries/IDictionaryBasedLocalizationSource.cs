using Abp.Localization.Sources;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Interface for a dictionary based localization source.
    /// �����ֵ�ı��ػ�Դ�ӿ�
    /// </summary>
    public interface IDictionaryBasedLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Gets the dictionary provider.
        /// ��ȡ�ֵ��ṩ��
        /// </summary>
        ILocalizationDictionaryProvider DictionaryProvider { get; }

        /// <summary>
        /// Extends the source with given dictionary.
        /// �ø������ֵ���չԴ
        /// </summary>
        /// <param name="dictionary">Dictionary to extend the source ��չԴ�ֵ�</param>
        void Extend(ILocalizationDictionary dictionary);
    }
}