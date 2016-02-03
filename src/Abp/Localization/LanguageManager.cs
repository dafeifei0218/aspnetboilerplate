using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Abp.Dependency;

namespace Abp.Localization
{
    /// <summary>
    /// ���Թ�����
    /// </summary>
    public class LanguageManager : ILanguageManager, ITransientDependency
    {
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public LanguageInfo CurrentLanguage { get { return GetCurrentLanguage(); } }

        private readonly ILanguageProvider _languageProvider;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="languageProvider">�����ṩ֮</param>
        public LanguageManager(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _languageProvider.GetLanguages();
        }

        /// <summary>
        /// ��ȡ��ǰ����
        /// </summary>
        /// <returns>������Ϣ</returns>
        private LanguageInfo GetCurrentLanguage()
        {
            var languages = _languageProvider.GetLanguages();
            if (languages.Count <= 0)
            {
                throw new AbpException("No language defined in this application.");
            }

            var currentCultureName = Thread.CurrentThread.CurrentUICulture.Name;

            //Try to find exact match
            //�����ҳ�׼ȷƥ��
            var currentLanguage = languages.FirstOrDefault(l => l.Name == currentCultureName);
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find best match
            //�����ҳ����ƥ��
            currentLanguage = languages.FirstOrDefault(l => currentCultureName.StartsWith(l.Name));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find default language
            //�����ҳ�Ĭ������
            currentLanguage = languages.FirstOrDefault(l => l.IsDefault);
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Get first one
            //��ȡ��һ��
            return languages[0];
        }
    }
}