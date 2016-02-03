using System.Collections.Generic;
using System.Threading;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// 空本地化管理类
    /// </summary>
    public class NullLocalizationManager : ILocalizationManager
    {
        /// <summary>
        /// Singleton instance.
        /// 单例实例
        /// </summary>
        public static NullLocalizationManager Instance { get { return SingletonInstance; } }
        private static readonly NullLocalizationManager SingletonInstance = new NullLocalizationManager();

        /// <summary>
        /// 当前语言
        /// </summary>
        public LanguageInfo CurrentLanguage { get { return new LanguageInfo(Thread.CurrentThread.CurrentUICulture.Name, Thread.CurrentThread.CurrentUICulture.DisplayName); } }
        
        //空语言信息列表
        private readonly IReadOnlyList<LanguageInfo> _emptyLanguageArray = new LanguageInfo[0];

        //空本地化源列表
        private readonly IReadOnlyList<ILocalizationSource> _emptyLocalizationSourceArray = new ILocalizationSource[0];

        /// <summary>
        /// 构造函数
        /// </summary>
        private NullLocalizationManager()
        {
            
        }

        /// <summary>
        /// 获取全部语言信息列表
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _emptyLanguageArray;
        }

        /// <summary>
        /// 获取本地化源
        /// </summary>
        /// <param name="name">本地化源名称</param>
        /// <returns></returns>
        public ILocalizationSource GetSource(string name)
        {
            return NullLocalizationSource.Instance;
        }

        /// <summary>
        /// 获取全部本地化源列表
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _emptyLocalizationSourceArray;
        }
    }
}