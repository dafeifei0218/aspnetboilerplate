using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// ���Թ�����
    /// </summary>
    public interface ILanguageManager
    {
        /// <summary>
        /// ��ǰ����
        /// </summary>
        LanguageInfo CurrentLanguage { get; }

        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}