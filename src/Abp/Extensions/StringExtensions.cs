using System;
using System.Globalization;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for String class.
    /// string字符串扩展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// 确认结束字符，如果不以字符结束，将给定的字符串添加到结束。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">字符</param>
        public static string EnsureEndsWith(this string str, char c)
        {
            return EnsureEndsWith(str, c, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// 确认结束字符，如果不以字符结束，将给定的字符串添加到结束。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">字符</param>
        /// <param name="comparisonType">比较类型</param>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.EndsWith(c.ToString(CultureInfo.InvariantCulture), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// 确认结束字符，如果不以字符结束，将给定的字符串添加到结束。
        /// </summary>
        /// <param name="str">要与此实例末尾的子字符串进行比较的字符串。</param>
        /// <param name="c">字符</param>
        /// <param name="ignoreCase">要在比较过程中忽略大小写，则为 true；否则为 false。</param>
        /// <param name="culture">确定如何对此实例与 value 进行比较的区域性信息。 如果 culture 为 null，则使用当前区域性。</param>
        public static string EnsureEndsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.EndsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// Adds a char to beginning of given string if it does not starts with the char.
        /// 如果不是从字符开始的话，在给定字符串的开头添加一个字符。
        /// </summary>
        /// <param name="str">要与此实例末尾的子字符串进行比较的字符串。</param>
        /// <param name="c">字符</param>
        public static string EnsureStartsWith(this string str, char c)
        {
            return EnsureStartsWith(str, c, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Adds a char to beginning of given string if it does not starts with the char.
        /// 如果不是从字符开始的话，在给定字符串的开头添加一个字符。
        /// </summary>
        /// <param name="str">要与此实例末尾的子字符串进行比较的字符串。</param>
        /// <param name="c">字符</param>
        /// <param name="comparisonType">比较类型</param>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.StartsWith(c.ToString(CultureInfo.InvariantCulture), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// Adds a char to beginning of given string if it does not starts with the char.
        /// 如果不是从字符开始的话，在给定字符串的开头添加一个字符。
        /// </summary>
        /// <param name="str">要与此实例末尾的子字符串进行比较的字符串。</param>
        /// <param name="c">字符</param>
        /// <param name="ignoreCase">要在比较过程中忽略大小写，则为 true；否则为 false。</param>
        /// <param name="culture">确定如何对此实例与 value 进行比较的区域性信息。 如果 culture 为 null，则使用当前区域性。</param>
        public static string EnsureStartsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.StartsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// Indicates whether this string is null or an System.String.Empty string.
        /// 指示此字符串是否为null或System.String.Empty字符串。
        /// </summary>
        /// <param name="str">字符串</param> 
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// indicates whether this string is null, empty, or consists only of white-space characters.
        /// 指示该字符串是否为null，Empty，或由空格字符组成。
        /// </summary>
        /// <param name="str">字符串</param> 
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// 获取指定长度的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">长度</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null 如果字符串为null，则抛出异常</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length 如果字符串长度小于指定长度，则抛出异常</exception>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// Converts line endings in the string to <see cref="Environment.NewLine"/>.
        /// 规范行结尾，将字符串中的行结尾转换为Environment.NewLine（获取为此环境定义的换行字符串）
        /// </summary>
        /// <param name="str">字符串</param>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Gets index of nth occurence of a char in a string.
        /// 获取字符出现在一个字符串的出现指定次数的索引
        /// </summary>
        /// <param name="str">source string to be searched 搜索源字符串</param>
        /// <param name="c">Char to search in <see cref="str"/> 搜索的字符串</param>
        /// <param name="n">Count of the occurence 出现次数</param>
        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets a substring of a string from end of the string.
        /// 获取指定长度的结束字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">长度</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null 如果字符串为null，则抛出异常</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length 如果字符串长度小于指定长度，则抛出异常</exception>
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// 用给定的分隔符拆分给定的字符串。
        /// </summary>
        /// <param name="str">待分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// 用给定的分隔符拆分给定的字符串。
        /// </summary>
        /// <param name="str">待分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="options">包含还是忽略空字符串</param>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// 拆分行，用Environment.NewLine换行符，分隔字符串
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// 拆分行，用Environment.NewLine换行符，分隔字符串
        /// </summary>
        /// <param name="str">待分隔的字符串</param>
        /// <param name="options">包含还是忽略空字符串</param>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string.
        /// 将字符串转换为PascalCase帕斯卡命名法
        /// </summary>
        /// <param name="str">String to convert 转换字符串</param>
        /// <returns>camelCase of the string 帕斯卡命名法的字符串</returns>
        public static string ToCamelCase(this string str)
        {
            return str.ToCamelCase(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string in specified culture.
        /// 
        /// </summary>
        /// <param name="str">String to convert 转换字符串</param>
        /// <param name="culture">An object that supplies culture-specific casing rules</param>
        /// <returns>camelCase of the string</returns>
        public static string ToCamelCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToLower(culture);
            }

            return char.ToLower(str[0], culture) + str.Substring(1);
        }

        /// <summary>
        /// Converts string to enum value.
        /// 转换字符串为枚举值
        /// </summary>
        /// <typeparam name="T">Type of enum 枚举类型</typeparam>
        /// <param name="value">String value to convert 转换字符串值</param>
        /// <returns>Returns enum object 返回枚举对象</returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Converts string to enum value.
        /// 转换字符串为枚举值
        /// </summary>
        /// <typeparam name="T">Type of enum 枚举类型</typeparam>
        /// <param name="value">String value to convert 转换字符串值</param>
        /// <param name="ignoreCase">Ignore case 是否区分大小写</param>
        /// <returns>Returns enum object 返回枚举对象</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// Converts camelCase string to PascalCase string.
        /// 
        /// </summary>
        /// <param name="str">String to convert 转换字符串</param>
        /// <returns>PascalCase of the string 帕斯卡命名的字符串</returns>
        public static string ToPascalCase(this string str)
        {
            return str.ToPascalCase(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts camelCase string to PascalCase string in specified culture.
        /// 
        /// </summary>
        /// <param name="str">String to convert 转换字符串</param>
        /// <param name="culture">An object that supplies culture-specific casing rules</param>
        /// <returns>PascalCase of the string 帕斯卡命名法的字符串</returns>
        public static string ToPascalCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToUpper(culture);
            }

            return char.ToUpper(str[0], culture) + str.Substring(1);
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// 截断，获取一个指定长度的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null 如果字符串为null，则抛出异常</exception>
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Left(maxLength);
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds a "..." postfix to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// 截断后添加“...”，获取一个指定长度的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null 如果字符串为null，则抛出异常</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds given <paramref name="postfix"/> to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// 截断后添加后缀，获取一个指定长度的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="postfix">后缀</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null 如果字符串为null，则抛出异常</exception>
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }
    }
}