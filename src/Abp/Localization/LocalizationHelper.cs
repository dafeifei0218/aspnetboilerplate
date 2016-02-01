using System;
using System.Globalization;
using Abp.Dependency;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// This static class is used to simplify getting localized strings.
    /// ���ػ�������
    /// </summary>
    public static class LocalizationHelper
    {
        /// <summary>
        /// Gets a reference to the localization manager.
        /// Inject and use <see cref="ILocalizationManager"/>
        /// wherever it's possible, instead of this shortcut.
        /// ���ػ�������
        /// </summary>
        public static ILocalizationManager Manager { get { return LocalizationManager.Value; } }

        private static readonly Lazy<ILocalizationManager> LocalizationManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        static LocalizationHelper()
        {
            LocalizationManager = new Lazy<ILocalizationManager>(
                () => IocManager.Instance.IsRegistered<ILocalizationManager>()
                    ? IocManager.Instance.Resolve<ILocalizationManager>()
                    : NullLocalizationManager.Instance
                );
        }

        /// <summary>
        /// Gets a pre-registered localization source.
        /// ��ȡ���ػ�Դ����ȡԤ��ע��ı��ػ�Դ
        /// </summary>
        /// <param name="name">���ػ�Դ����</param>
        public static ILocalizationSource GetSource(string name)
        {
            return LocalizationManager.Value.GetSource(name);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// ��ȡ��ǰ���Եı��ػ��ַ���
        /// </summary>
        /// <param name="sourceName">Name of the localization source Դ����</param>
        /// <param name="name">Key name to get localized string ���ػ��ַ���������</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        public static string GetString(string sourceName, string name)
        {
            return LocalizationManager.Value.GetString(sourceName, name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// ��ȡ��ǰ���Եı��ػ��ַ���
        /// </summary>
        /// <param name="sourceName">Name of the localization source Դ����</param>
        /// <param name="name">Key name to get localized string ���ػ��ַ���������</param>
        /// <param name="culture">culture �й��ض���������Ϣ</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        public static string GetString(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture);
        }
    }
}