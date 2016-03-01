using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Text.Formatting;

namespace Abp.Text
{
    /// <summary>
    /// This class is used to extract dynamic values from a formatted string.
    /// It works as reverse of <see cref="string.Format(string,object)"/>
    /// ��ʽ���ַ�����ȡ
    /// </summary>
    /// <example>
    /// Say that str is "My name is Neo." and format is "My name is {name}.".
    /// Then Extract method gets "Neo" as "name".  
    /// �����ַ�����"My name is Neo.",��ʽ����Ϊ"My name is {name}." 
    /// Ȼ����ȡ������ȡ"Neo" ��Ϊ "name"
    /// </example>
    public class FormattedStringValueExtracter
    {
        /// <summary>
        /// Extracts dynamic values from a formatted string.
        /// ��ȡ���Ӹ�ʽ�����ַ�������ȡ��ֵ̬����.
        /// </summary>
        /// <param name="str">String including dynamic values ������ֵ̬�Ķ���</param>
        /// <param name="format">Format of the string ��ʽ������ַ���</param>
        /// <param name="ignoreCase">True, to search case-insensitive. true,����ʱ�����Դ�Сд��</param>
        public ExtractionResult Extract(string str, string format, bool ignoreCase = false)
        {
            var stringComparison = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            if (str == format) //TODO: think on that!
            {
                return new ExtractionResult(true);
            }

            var formatTokens = new FormatStringTokenizer().Tokenize(format);
            if (formatTokens.IsNullOrEmpty())
            {
                return new ExtractionResult(str == "");
            }

            var result = new ExtractionResult(true);

            for (var i = 0; i < formatTokens.Count; i++)
            {
                var currentToken = formatTokens[i];
                var previousToken = i > 0 ? formatTokens[i - 1] : null;

                if (currentToken.Type == FormatStringTokenType.ConstantText)
                {
                    if (i == 0)
                    {
                        if (!str.StartsWith(currentToken.Text, stringComparison))
                        {
                            result.IsMatch = false;
                            return result;
                        }

                        str = str.Substring(currentToken.Text.Length);
                    }
                    else
                    {
                        var matchIndex = str.IndexOf(currentToken.Text, stringComparison);
                        if (matchIndex < 0)
                        {
                            result.IsMatch = false;
                            return result;
                        }

                        Debug.Assert(previousToken != null, "previousToken can not be null since i > 0 here");

                        result.Matches.Add(new NameValue(previousToken.Text, str.Substring(0, matchIndex)));
                        str = str.Substring(matchIndex + currentToken.Text.Length);
                    }
                }
            }

            var lastToken = formatTokens.Last();
            if (lastToken.Type == FormatStringTokenType.DynamicValue)
            {
                result.Matches.Add(new NameValue(lastToken.Text, str));
            }

            return result;
        }

        /// <summary>
        /// Checks if given <see cref="str"/> fits to given <see cref="format"/>.
        /// Also gets extracted values.
        /// �Ƿ�ƥ�䣬
        /// ��������<see cref="str"/> �Ƿ���ϸ�����<see cref="format"/>.
        /// ͬʱ����ȡֵ��
        /// </summary>
        /// <param name="str">String including dynamic values ������̬ʱ���ַ���</param>
        /// <param name="format">Format of the string �ַ�����ʽ</param>
        /// <param name="values">Array of extracted values if matched ��ȡֵ�����飨���ƥ�䣩</param>
        /// <param name="ignoreCase">True, to search case-insensitive. true,���Դ�Сд</param>
        /// <returns>True, if matched. true,���ƥ��</returns>
        public static bool IsMatch(string str, string format, out string[] values, bool ignoreCase = false)
        {
            var result = new FormattedStringValueExtracter().Extract(str, format, ignoreCase);
            if (!result.IsMatch)
            {
                values = new string[0];
                return false;
            }

            values = result.Matches.Select(m => m.Value).ToArray();
            return true;
        }

        /// <summary>
        /// Used as return value of <see cref="Extract"/> method.
        /// ��ȡ�������������<see cref="Extract"/>�ķ���ֵ����.
        /// </summary>
        public class ExtractionResult
        {
            /// <summary>
            /// Is fully matched.
            /// �Ƿ�ƥ��
            /// </summary>
            public bool IsMatch { get; set; }

            /// <summary>
            /// List of matched dynamic values.
            /// ƥ��Ķ�ֵ̬�б�
            /// </summary>
            public List<NameValue> Matches { get; private set; }

            /// <summary>
            /// ���캯��
            /// </summary>
            /// <param name="isMatch"></param>
            internal ExtractionResult(bool isMatch)
            {
                IsMatch = isMatch;
                Matches = new List<NameValue>();
            }
        }
    }
}