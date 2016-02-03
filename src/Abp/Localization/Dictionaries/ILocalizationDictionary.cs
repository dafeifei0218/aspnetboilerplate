using System.Collections.Generic;
using System.Globalization;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Represents a dictionary that is used to find a localized string.
    /// 本地化字典
    /// </summary>
    public interface ILocalizationDictionary
    {
        /// <summary>
        /// Culture of the dictionary.
        /// 字典特定区域信息
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        /// Gets/sets a string for this dictionary with given name (key).
        /// 索引器
        /// </summary>
        /// <param name="name">Name to get/set 名称的获取/设置</param>
        string this[string name] { get; set; }

        /// <summary>
        /// Gets a <see cref="LocalizedString"/> for given <paramref name="name"/>.
        /// 根据给定的键名称，获取本地化字符串
        /// </summary>
        /// <param name="name">Name (key) to get localized string 本地化字符串键名称</param>
        /// <returns>The localized string or null if not found in this dictionary 本地化字符串</returns>
        LocalizedString GetOrNull(string name);

        /// <summary>
        /// Gets a list of all strings in this dictionary.
        /// 获取全部字典字符串
        /// </summary>
        /// <returns>List of all <see cref="LocalizedString"/> object 本地化字符串列表</returns>
        IReadOnlyList<LocalizedString> GetAllStrings();
    }
}