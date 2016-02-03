using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Abp.Dependency;

namespace Abp.Localization
{
    /// <summary>
    /// 语言管理类
    /// </summary>
    public class LanguageManager : ILanguageManager, ITransientDependency
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        public LanguageInfo CurrentLanguage { get { return GetCurrentLanguage(); } }

        private readonly ILanguageProvider _languageProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageProvider">语言提供之</param>
        public LanguageManager(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        /// <summary>
        /// 获取语言信息列表
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _languageProvider.GetLanguages();
        }

        /// <summary>
        /// 获取当前语言
        /// </summary>
        /// <returns>语言信息</returns>
        private LanguageInfo GetCurrentLanguage()
        {
            var languages = _languageProvider.GetLanguages();
            if (languages.Count <= 0)
            {
                throw new AbpException("No language defined in this application.");
            }

            var currentCultureName = Thread.CurrentThread.CurrentUICulture.Name;

            //Try to find exact match
            //试着找出准确匹配
            var currentLanguage = languages.FirstOrDefault(l => l.Name == currentCultureName);
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find best match
            //试着找出最佳匹配
            currentLanguage = languages.FirstOrDefault(l => currentCultureName.StartsWith(l.Name));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find default language
            //试着找出默认语言
            currentLanguage = languages.FirstOrDefault(l => l.IsDefault);
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Get first one
            //获取第一个
            return languages[0];
        }
    }
}