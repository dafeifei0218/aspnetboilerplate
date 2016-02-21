using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a string that can be localized when needed.
    /// ���ػ��ַ�����
    /// </summary>
    public interface ILocalizableString
    {
        ///// <summary>
        ///// Localizes the string in current culture.
        ///// ��ǰ���Եı��ػ��ַ���
        ///// </summary>
        ///// <returns>Localized string ���ػ��ַ���</returns>
        //string Localize();

        /// <summary>
        /// Localizes the string in current culture.
        /// </summary>
        /// <param name="context">Localization context</param>
        /// <returns>Localized string</returns>
        string Localize(ILocalizationContext context);


        /// <summary>
        /// Localizes the string in given culture.
        /// ���ػ��ַ���
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture">culture �ṩ�й��ض������Ե���Ϣ</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        string Localize(ILocalizationContext context, CultureInfo culture);
    }
}