using Abp.Localization.Sources;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Interface for a dictionary based localization source.
    /// 基于字典的本地化源接口
    /// </summary>
    public interface IDictionaryBasedLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Gets the dictionary provider.
        /// 获取字典提供者
        /// </summary>
        ILocalizationDictionaryProvider DictionaryProvider { get; }

        /// <summary>
        /// Extends the source with given dictionary.
        /// 用给定的字典扩展源
        /// </summary>
        /// <param name="dictionary">Dictionary to extend the source 扩展源字典</param>
        void Extend(ILocalizationDictionary dictionary);
    }
}