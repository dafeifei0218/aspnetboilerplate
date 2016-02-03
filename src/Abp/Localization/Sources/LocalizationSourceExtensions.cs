using System;
using System.Globalization;

namespace Abp.Localization.Sources
{
    /// <summary>
    /// Extension methods for <see cref="ILocalizationSource"/>.
    /// 本地化源扩展方法
    /// </summary>
    public static class LocalizationSourceExtensions
    {
        /// <summary>
        /// Get a localized string by formatting string.
        /// 用格式化的字符串获取本地化的字符串
        /// </summary>
        /// <param name="source">Localization source 本地化源</param>
        /// <param name="name">Key name 键名称</param>
        /// <param name="args">Format arguments 格式化参数</param>
        /// <returns>Formatted and localized string 格式化和本地化字符串</returns>
        public static string GetString(this ILocalizationSource source, string name, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name), args);
        }

        /// <summary>
        /// Get a localized string in given language by formatting string.
        /// 用格式化字符串在给定的语言中得到一个本地化的字符串。
        /// </summary>
        /// <param name="source">Localization source 本地化源</param>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">Culture 提供有关特定区域性的信息</param>
        /// <param name="args">Format arguments 格式化参数</param>
        /// <returns>Formatted and localized string 格式化和本地化字符串</returns>
        public static string GetString(this ILocalizationSource source, string name, CultureInfo culture, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name, culture), args);
        }
    }
}