using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a localized string.
    /// ���ػ��ַ���
    /// </summary>
    public class LocalizedString
    {
        /// <summary>
        /// Culture info for this string.
        /// �ṩ�й��ض������Ե���Ϣ
        /// </summary>
        public CultureInfo CultureInfo { get; internal set; }

        /// <summary>
        /// Unique Name of the string.
        /// ���ػ��ַ���������
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value for the <see cref="Name"/>.
        /// ���ػ��ַ��������Ƶ�ֵ
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a localized string instance.
        /// ���캯��
        /// </summary>
        /// <param name="cultureInfo">Culture info for this string �ṩ�й��ض������Ե���Ϣ</param>
        /// <param name="name">Unique Name of the string ���ػ��ַ���������</param>
        /// <param name="value">Value for the <paramref name="name"/> ���ػ��ַ��������Ƶ�ֵ</param>
        public LocalizedString(string name, string value, CultureInfo cultureInfo)
        {
            Name = name;
            Value = value;
            CultureInfo = cultureInfo;
        }
    }
}