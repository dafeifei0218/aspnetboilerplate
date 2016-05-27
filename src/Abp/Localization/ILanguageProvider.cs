using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// 语言提供者接口
    /// </summary>
    /// <remarks>
    /// 接口定义一个返回本地语言集合的方法。
    /// 这里使用接口做隔离是有必要的，因为ABP底层框架的DefaultLanguageProvider只是返回用过代码HardCode到系统中的LanguageInfo信息。
    /// 如果需要从其他Source（比如数据库）中获取配置的LanguageInfo信息，那么我们就必须实现自定义的LanguageProvider。
    /// </remarks>
    public interface ILanguageProvider
    {
        /// <summary>
        /// 获取语言信息列表
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}