using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// 语言管理类
    /// </summary>
    public interface ILanguageManager
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        LanguageInfo CurrentLanguage { get; }

        /// <summary>
        /// 获取语言信息列表
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}