using System.Collections.Generic;
using System.Globalization;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization.Sources
{
    /// <summary>
    /// A Localization Source is used to obtain localized strings.
    /// ���ػ�Դ�ӿڣ����ػ�Դ��������ñ��ػ��ַ���
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// Unique Name of the source.
        /// ���ػ�Դ������
        /// </summary>
        string Name { get; }

        /// <summary>
        /// This method is called by ABP before first usage.
        /// ��ʼ��
        /// </summary>
        /// <param name="configuration">���ػ�����</param>
        /// <param name="iocResolver">IOC���Ʒ�ת������</param>
        void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Fallbacks to default language if not found in current culture.
        /// �ڵ�ǰ�����л�ȡ�������Ƶı��ػ��ַ�����
        /// �����ǰ�Ļ���δ�ҵ�������Ĭ�����ԡ�
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        string GetString(string name);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Fallbacks to default language if not found in given culture.
        /// �ڵ�ǰ�����л�ȡ�������ƺ���������Ϣ�ı��ػ��ַ�����
        /// �����ǰ�Ļ���δ�ҵ�������Ĭ�����ԡ�
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <param name="culture">culture information �ṩ�й��ض������Ե���Ϣ</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Returns null if not found.
        /// �ڵ�ǰ�����л�ȡ�������Ƶı��ػ��ַ�����
        /// ���δ�ҵ�����null��
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// true�������ǰ�Ļ���δ�ҵ�������Ĭ�����ԡ�
        /// </param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        string GetStringOrNull(string name, bool tryDefaults = true);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Returns null if not found.
        /// �ڵ�ǰ�����л�ȡ�������ƺ���������Ϣ�ı��ػ��ַ�����
        /// ���δ�ҵ�����null��
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <param name="culture">culture information �ṩ�й��ض������Ե���Ϣ</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// true�������ǰ�Ļ���δ�ҵ�������Ĭ�����ԡ�
        /// </param>
        /// <returns>Localized string</returns>
        string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// Gets all strings in current language.
        /// ��ȡ��ǰ���Ե�ȫ���ַ���
        /// </summary>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// true�������ǰ�Ļ���δ�ҵ�������Ĭ�����ԡ�
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true);

        /// <summary>
        /// Gets all strings in specified culture.
        /// ��ȡ��ǰ��������Ϣ��ȫ���ַ���
        /// </summary>
        /// <param name="culture">culture information �ṩ�й��ض������Ե���Ϣ</param>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// true�������ǰ�Ļ���δ�ҵ�������Ĭ�����ԡ�
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true);
    }
}