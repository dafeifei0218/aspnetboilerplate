using System;
using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a string that can be localized.
    /// 本地化字符串
    /// </summary>
    /// <remarks>
    /// 封装需要被本地化的string的信息，并提供Localize方法（调用ILocalizationManager的GetString方法）返回本地化的string。
    /// SourceName指定其从那个本地化资源读取本地化文本。
    /// </remarks>
    public class LocalizableString : ILocalizableString
    {
        /// <summary>
        /// Unique name of the localization source.
        /// 本地化源名称
        /// </summary>
        public virtual string SourceName { get; private set; }

        /// <summary>
        /// Unique Name of the string to be localized.
        /// 本地化名称
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="name">Unique name of the localization source 本地化源名称</param>
        /// <param name="sourceName">Unique Name of the string to be localized 本地化名称</param>
        public LocalizableString(string name, string sourceName)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (sourceName == null)
            {
                throw new ArgumentNullException("sourceName");
            }

            Name = name;
            SourceName = sourceName;
        }

        /// <summary>
        /// Localizes the string in current language.
        /// 当前语言的本地化字符串
        /// </summary>
        /// <returns>Localized string 本地化字符串</returns>
        public virtual string Localize(ILocalizationContext context)
        {
            return LocalizationHelper.GetString(SourceName, Name);
        }

        /// <summary>
        /// Localizes the string in current language.
        /// 本地化字符串
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture">culture 提供有关特定区域性的信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        public virtual string Localize(ILocalizationContext context,CultureInfo culture)
        {
            return LocalizationHelper.GetString(SourceName, Name, culture);
        }

        //public override string ToString()
        //{
        //    return Localize();
        //}
    }
}