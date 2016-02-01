using System.Collections.Generic;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// This interface is used to manage localization system.
    /// ���ػ�������
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// Gets current language for the application.
        /// ��ȡ��ǰ����
        /// </summary>
        LanguageInfo CurrentLanguage { get; }

        /// <summary>
        /// Gets all available languages for the application.
        /// ��ȡӦ�ó�������п�������
        /// </summary>
        /// <returns>List of languages ������Ϣ�б�</returns>
        IReadOnlyList<LanguageInfo> GetAllLanguages();

        /// <summary>
        /// Gets a localization source with name.
        /// ��ȡָ�����Ƶı��ػ�Դ
        /// </summary>
        /// <param name="name">Unique name of the localization source ���ػ�Դ����</param>
        /// <returns>The localization source ���ػ�Դ</returns>
        ILocalizationSource GetSource(string name);

        /// <summary>
        /// Gets all registered localization sources.
        /// ��ȡȫ�����ػ�Դ�б�
        /// </summary>
        /// <returns>List of sources ���ػ�Դ�б�</returns>
        IReadOnlyList<ILocalizationSource> GetAllSources();
    }
}