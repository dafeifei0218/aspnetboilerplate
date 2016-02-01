using System.Collections.Generic;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// This interface is used to manage localization system.
    /// 本地化管理类
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// Gets current language for the application.
        /// 获取当前语言
        /// </summary>
        LanguageInfo CurrentLanguage { get; }

        /// <summary>
        /// Gets all available languages for the application.
        /// 获取应用程序的所有可用语言
        /// </summary>
        /// <returns>List of languages 语言信息列表</returns>
        IReadOnlyList<LanguageInfo> GetAllLanguages();

        /// <summary>
        /// Gets a localization source with name.
        /// 获取指定名称的本地化源
        /// </summary>
        /// <param name="name">Unique name of the localization source 本地化源名称</param>
        /// <returns>The localization source 本地化源</returns>
        ILocalizationSource GetSource(string name);

        /// <summary>
        /// Gets all registered localization sources.
        /// 获取全部本地化源列表
        /// </summary>
        /// <returns>List of sources 本地化源列表</returns>
        IReadOnlyList<ILocalizationSource> GetAllSources();
    }
}