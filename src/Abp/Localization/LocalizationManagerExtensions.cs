using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// 本地化管理扩展类
    /// </summary>
    public static class LocalizationManagerExtensions
    {
        /// <summary>
        /// Gets a localized string in current language.
        /// 获取当前语言的本地化字符串
        /// </summary>
        /// <param name="localizationManager">本地化管理类</param>
        /// <param name="localizableString">本地化字符串</param>
        /// <returns>Localized string 本地化字符串</returns>
        public static string GetString(this ILocalizationManager localizationManager, LocalizableString localizableString)
        {
            return localizationManager.GetString(localizableString.SourceName, localizableString.Name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// 获取指定语言的本地化字符串
        /// </summary>
        /// <param name="localizationManager">本地化管理类</param>
        /// <param name="localizableString">本地化字符串</param>
        /// <param name="culture">提供有关特定区域性的信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        public static string GetString(this ILocalizationManager localizationManager, LocalizableString localizableString, CultureInfo culture)
        {
            return localizationManager.GetString(localizableString.SourceName, localizableString.Name, culture);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// 获取当前语言的本地化字符串
        /// </summary>
        /// <param name="localizationManager">Localization manager instance 本地化管理类</param>
        /// <param name="sourceName">Name of the localization source 本地化源名称</param>
        /// <param name="name">Key name to get localized string 本地化字符串键名称</param>
        /// <returns>Localized string 本地化字符串</returns>
        public static string GetString(this ILocalizationManager localizationManager, string sourceName, string name)
        {
            return localizationManager.GetSource(sourceName).GetString(name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// 获取指定语言的本地化字符串
        /// </summary>
        /// <param name="localizationManager">Localization manager instance 本地化管理类</param>
        /// <param name="sourceName">Name of the localization source 本地化源名称</param>
        /// <param name="name">Key name to get localized string 本地化字符串键名称</param>
        /// <param name="culture">culture 提供有关特定区域性的信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        public static string GetString(this ILocalizationManager localizationManager, string sourceName, string name, CultureInfo culture)
        {
            return localizationManager.GetSource(sourceName).GetString(name, culture);
        }
    }
}