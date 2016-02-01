using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// A class that gets the same string on every localization.
    /// 固定的本地化字符串，在每个本地化上都得到相同字符串的类
    /// </summary>
    public class FixedLocalizableString : ILocalizableString
    {
        /// <summary>
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// 固定字符串，每当本地化方法调用时，返回此字符串。
        /// </summary>
        public virtual string FixedString { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="FixedLocalizableString"/>.
        /// 构造函数
        /// </summary>
        /// <param name="fixedString">
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// 固定字符串
        /// </param>
        public FixedLocalizableString(string fixedString)
        {
            FixedString = fixedString;
        }

        /// <summary>
        /// Gets the <see cref="FixedString"/> always.
        /// 获取固定字符串
        /// </summary>
        public virtual string Localize()
        {
            return FixedString;
        }

        /// <summary>
        /// Gets the <see cref="FixedString"/> always.
        /// 获取固定字符串
        /// </summary>
        /// <param name="culture">提供有关特定区域性的信息</param>
        public virtual string Localize(CultureInfo culture)
        {
            return FixedString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FixedString;
        }
    }
}