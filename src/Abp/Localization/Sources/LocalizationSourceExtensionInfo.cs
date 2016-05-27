using Abp.Localization.Dictionaries;

namespace Abp.Localization.Sources
{
    /// <summary>
    /// Used to store a localization source extension information.
    /// 本地化源扩展信息
    /// </summary>
    /// <remarks>
    /// ABP在LocalizationManager初始化的过程中将LocalizationSourceExtensionInfo所对应的本地化资源，
    /// 扩充到ILocalizationSource对象的响应本地化资源字典中。
    /// </remarks>
    public class LocalizationSourceExtensionInfo
    {
        /// <summary>
        /// Source name.
        /// 本地化源名称
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        /// Extension dictionaries.
        /// 扩展字典提供者
        /// </summary>
        public ILocalizationDictionaryProvider DictionaryProvider { get; private set; }

        /// <summary>
        /// Creates a new <see cref="LocalizationSourceExtensionInfo"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="sourceName">Source name本地化源名称</param>
        /// <param name="dictionaryProvider">Extension dictionaries 扩展字典提供者</param>
        public LocalizationSourceExtensionInfo(string sourceName, ILocalizationDictionaryProvider dictionaryProvider)
        {
            SourceName = sourceName;
            DictionaryProvider = dictionaryProvider;
        }
    }
}