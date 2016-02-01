using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a string that can be localized when needed.
    /// 本地化字符串，
    /// </summary>
    public interface ILocalizableString
    {
        /// <summary>
        /// Localizes the string in current culture.
        /// 当前语言的本地化字符串
        /// </summary>
        /// <returns>Localized string 本地化字符串</returns>
        string Localize();

        /// <summary>
        /// Localizes the string in given culture.
        /// 本地化字符串
        /// </summary>
        /// <param name="culture">culture 提供有关特定区域性的信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        string Localize(CultureInfo culture);
    }
}