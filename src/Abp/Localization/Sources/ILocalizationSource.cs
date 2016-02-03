using System.Collections.Generic;
using System.Globalization;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization.Sources
{
    /// <summary>
    /// A Localization Source is used to obtain localized strings.
    /// 本地化源接口，本地化源是用来获得本地化字符串
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// Unique Name of the source.
        /// 本地化源的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// This method is called by ABP before first usage.
        /// 初始化
        /// </summary>
        /// <param name="configuration">本地化配置</param>
        /// <param name="iocResolver">IOC控制反转解析器</param>
        void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Fallbacks to default language if not found in current culture.
        /// 在当前语言中获取给定名称的本地化字符串。
        /// 如果当前文化中未找到，返回默认语言。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <returns>Localized string 本地化字符串</returns>
        string GetString(string name);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Fallbacks to default language if not found in given culture.
        /// 在当前语言中获取给定名称和区域性信息的本地化字符串。
        /// 如果当前文化中未找到，返回默认语言。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 提供有关特定区域性的信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Returns null if not found.
        /// 在当前语言中获取给定名称的本地化字符串。
        /// 如果未找到返回null。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// true：如果当前文化中未找到，返回默认语言。
        /// </param>
        /// <returns>Localized string 本地化字符串</returns>
        string GetStringOrNull(string name, bool tryDefaults = true);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Returns null if not found.
        /// 在当前语言中获取给定名称和区域性信息的本地化字符串。
        /// 如果未找到返回null。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 提供有关特定区域性的信息</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// true：如果当前文化中未找到，返回默认语言。
        /// </param>
        /// <returns>Localized string</returns>
        string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// Gets all strings in current language.
        /// 获取当前语言的全部字符串
        /// </summary>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// true：如果当前文化中未找到，返回默认语言。
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true);

        /// <summary>
        /// Gets all strings in specified culture.
        /// 获取当前区域性信息的全部字符串
        /// </summary>
        /// <param name="culture">culture information 提供有关特定区域性的信息</param>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// true：如果当前文化中未找到，返回默认语言。
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true);
    }
}