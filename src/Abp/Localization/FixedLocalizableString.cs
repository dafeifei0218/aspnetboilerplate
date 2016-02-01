using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// A class that gets the same string on every localization.
    /// �̶��ı��ػ��ַ�������ÿ�����ػ��϶��õ���ͬ�ַ�������
    /// </summary>
    public class FixedLocalizableString : ILocalizableString
    {
        /// <summary>
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// �̶��ַ�����ÿ�����ػ���������ʱ�����ش��ַ�����
        /// </summary>
        public virtual string FixedString { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="FixedLocalizableString"/>.
        /// ���캯��
        /// </summary>
        /// <param name="fixedString">
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// �̶��ַ���
        /// </param>
        public FixedLocalizableString(string fixedString)
        {
            FixedString = fixedString;
        }

        /// <summary>
        /// Gets the <see cref="FixedString"/> always.
        /// ��ȡ�̶��ַ���
        /// </summary>
        public virtual string Localize()
        {
            return FixedString;
        }

        /// <summary>
        /// Gets the <see cref="FixedString"/> always.
        /// ��ȡ�̶��ַ���
        /// </summary>
        /// <param name="culture">�ṩ�й��ض������Ե���Ϣ</param>
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