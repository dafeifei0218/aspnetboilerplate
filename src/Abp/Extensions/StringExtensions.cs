using System;
using System.Globalization;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for String class.
    /// string�ַ�����չ��
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// ȷ�Ͻ����ַ�����������ַ����������������ַ�����ӵ�������
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="c">�ַ�</param>
        public static string EnsureEndsWith(this string str, char c)
        {
            return EnsureEndsWith(str, c, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Adds a char to end of given string if it does not ends with the char.
        /// ȷ�Ͻ����ַ�����������ַ����������������ַ�����ӵ�������
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="c">�ַ�</param>
        /// <param name="comparisonType">�Ƚ�����</param>
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
        /// ȷ�Ͻ����ַ�����������ַ����������������ַ�����ӵ�������
        /// </summary>
        /// <param name="str">Ҫ���ʵ��ĩβ�����ַ������бȽϵ��ַ�����</param>
        /// <param name="c">�ַ�</param>
        /// <param name="ignoreCase">Ҫ�ڱȽϹ����к��Դ�Сд����Ϊ true������Ϊ false��</param>
        /// <param name="culture">ȷ����ζԴ�ʵ���� value ���бȽϵ���������Ϣ�� ��� culture Ϊ null����ʹ�õ�ǰ�����ԡ�</param>
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
        /// ������Ǵ��ַ���ʼ�Ļ����ڸ����ַ����Ŀ�ͷ���һ���ַ���
        /// </summary>
        /// <param name="str">Ҫ���ʵ��ĩβ�����ַ������бȽϵ��ַ�����</param>
        /// <param name="c">�ַ�</param>
        public static string EnsureStartsWith(this string str, char c)
        {
            return EnsureStartsWith(str, c, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Adds a char to beginning of given string if it does not starts with the char.
        /// ������Ǵ��ַ���ʼ�Ļ����ڸ����ַ����Ŀ�ͷ���һ���ַ���
        /// </summary>
        /// <param name="str">Ҫ���ʵ��ĩβ�����ַ������бȽϵ��ַ�����</param>
        /// <param name="c">�ַ�</param>
        /// <param name="comparisonType">�Ƚ�����</param>
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
        /// ������Ǵ��ַ���ʼ�Ļ����ڸ����ַ����Ŀ�ͷ���һ���ַ���
        /// </summary>
        /// <param name="str">Ҫ���ʵ��ĩβ�����ַ������бȽϵ��ַ�����</param>
        /// <param name="c">�ַ�</param>
        /// <param name="ignoreCase">Ҫ�ڱȽϹ����к��Դ�Сд����Ϊ true������Ϊ false��</param>
        /// <param name="culture">ȷ����ζԴ�ʵ���� value ���бȽϵ���������Ϣ�� ��� culture Ϊ null����ʹ�õ�ǰ�����ԡ�</param>
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
        /// ָʾ���ַ����Ƿ�Ϊnull��System.String.Empty�ַ�����
        /// </summary>
        /// <param name="str">�ַ���</param> 
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// indicates whether this string is null, empty, or consists only of white-space characters.
        /// ָʾ���ַ����Ƿ�Ϊnull��Empty�����ɿո��ַ���ɡ�
        /// </summary>
        /// <param name="str">�ַ���</param> 
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// ��ȡָ�����ȵ��ַ���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="len">����</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null ����ַ���Ϊnull�����׳��쳣</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length ����ַ�������С��ָ�����ȣ����׳��쳣</exception>
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
        /// �淶�н�β�����ַ����е��н�βת��ΪEnvironment.NewLine����ȡΪ�˻�������Ļ����ַ�����
        /// </summary>
        /// <param name="str">�ַ���</param>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Gets index of nth occurence of a char in a string.
        /// ��ȡ�ַ�������һ���ַ����ĳ���ָ������������
        /// </summary>
        /// <param name="str">source string to be searched ����Դ�ַ���</param>
        /// <param name="c">Char to search in <see cref="str"/> �������ַ���</param>
        /// <param name="n">Count of the occurence ���ִ���</param>
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
        /// ��ȡָ�����ȵĽ����ַ���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="len">����</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null ����ַ���Ϊnull�����׳��쳣</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length ����ַ�������С��ָ�����ȣ����׳��쳣</exception>
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
        /// �ø����ķָ�����ָ������ַ�����
        /// </summary>
        /// <param name="str">���ָ����ַ���</param>
        /// <param name="separator">�ָ���</param>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// �ø����ķָ�����ָ������ַ�����
        /// </summary>
        /// <param name="str">���ָ����ַ���</param>
        /// <param name="separator">�ָ���</param>
        /// <param name="options">�������Ǻ��Կ��ַ���</param>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// ����У���Environment.NewLine���з����ָ��ַ���
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// ����У���Environment.NewLine���з����ָ��ַ���
        /// </summary>
        /// <param name="str">���ָ����ַ���</param>
        /// <param name="options">�������Ǻ��Կ��ַ���</param>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string.
        /// ���ַ���ת��ΪPascalCase��˹��������
        /// </summary>
        /// <param name="str">String to convert ת���ַ���</param>
        /// <returns>camelCase of the string ��˹�����������ַ���</returns>
        public static string ToCamelCase(this string str)
        {
            return str.ToCamelCase(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string in specified culture.
        /// 
        /// </summary>
        /// <param name="str">String to convert ת���ַ���</param>
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
        /// ת���ַ���Ϊö��ֵ
        /// </summary>
        /// <typeparam name="T">Type of enum ö������</typeparam>
        /// <param name="value">String value to convert ת���ַ���ֵ</param>
        /// <returns>Returns enum object ����ö�ٶ���</returns>
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
        /// ת���ַ���Ϊö��ֵ
        /// </summary>
        /// <typeparam name="T">Type of enum ö������</typeparam>
        /// <param name="value">String value to convert ת���ַ���ֵ</param>
        /// <param name="ignoreCase">Ignore case �Ƿ����ִ�Сд</param>
        /// <returns>Returns enum object ����ö�ٶ���</returns>
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
        /// <param name="str">String to convert ת���ַ���</param>
        /// <returns>PascalCase of the string ��˹���������ַ���</returns>
        public static string ToPascalCase(this string str)
        {
            return str.ToPascalCase(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts camelCase string to PascalCase string in specified culture.
        /// 
        /// </summary>
        /// <param name="str">String to convert ת���ַ���</param>
        /// <param name="culture">An object that supplies culture-specific casing rules</param>
        /// <returns>PascalCase of the string ��˹�����������ַ���</returns>
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
        /// �ضϣ���ȡһ��ָ�����ȵ��ַ���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null ����ַ���Ϊnull�����׳��쳣</exception>
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
        /// �ضϺ���ӡ�...������ȡһ��ָ�����ȵ��ַ���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null ����ַ���Ϊnull�����׳��쳣</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds given <paramref name="postfix"/> to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// �ضϺ���Ӻ�׺����ȡһ��ָ�����ȵ��ַ���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="postfix">��׺</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null ����ַ���Ϊnull�����׳��쳣</exception>
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