using System;
using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a string that can be localized.
    /// ���ػ��ַ���
    /// </summary>
    /// <remarks>
    /// ��װ��Ҫ�����ػ���string����Ϣ�����ṩLocalize����������ILocalizationManager��GetString���������ر��ػ���string��
    /// SourceNameָ������Ǹ����ػ���Դ��ȡ���ػ��ı���
    /// </remarks>
    public class LocalizableString : ILocalizableString
    {
        /// <summary>
        /// Unique name of the localization source.
        /// ���ػ�Դ����
        /// </summary>
        public virtual string SourceName { get; private set; }

        /// <summary>
        /// Unique Name of the string to be localized.
        /// ���ػ�����
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        ///  ���캯��
        /// </summary>
        /// <param name="name">Unique name of the localization source ���ػ�Դ����</param>
        /// <param name="sourceName">Unique Name of the string to be localized ���ػ�����</param>
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
        /// ��ǰ���Եı��ػ��ַ���
        /// </summary>
        /// <returns>Localized string ���ػ��ַ���</returns>
        public virtual string Localize(ILocalizationContext context)
        {
            return LocalizationHelper.GetString(SourceName, Name);
        }

        /// <summary>
        /// Localizes the string in current language.
        /// ���ػ��ַ���
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture">culture �ṩ�й��ض������Ե���Ϣ</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
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