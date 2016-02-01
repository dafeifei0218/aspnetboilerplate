using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a localized string.
    /// 本地化字符串
    /// </summary>
    public class LocalizedString
    {
        /// <summary>
        /// Culture info for this string.
        /// 提供有关特定区域性的信息
        /// </summary>
        public CultureInfo CultureInfo { get; internal set; }

        /// <summary>
        /// Unique Name of the string.
        /// 本地化字符串键名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value for the <see cref="Name"/>.
        /// 本地化字符串键名称的值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a localized string instance.
        /// 构造函数
        /// </summary>
        /// <param name="cultureInfo">Culture info for this string 提供有关特定区域性的信息</param>
        /// <param name="name">Unique Name of the string 本地化字符串键名称</param>
        /// <param name="value">Value for the <paramref name="name"/> 本地化字符串键名称的值</param>
        public LocalizedString(string name, string value, CultureInfo cultureInfo)
        {
            Name = name;
            Value = value;
            CultureInfo = cultureInfo;
        }
    }
}