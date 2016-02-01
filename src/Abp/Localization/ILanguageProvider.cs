using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// 语言提供者接口
    /// </summary>
    public interface ILanguageProvider
    {
        /// <summary>
        /// 获取语言信息列表
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}