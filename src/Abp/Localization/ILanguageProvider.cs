using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// �����ṩ�߽ӿ�
    /// </summary>
    public interface ILanguageProvider
    {
        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}