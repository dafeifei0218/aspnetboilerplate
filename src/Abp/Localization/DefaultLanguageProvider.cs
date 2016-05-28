using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization
{
    /// <summary>
    /// 默认语言提供者 
    /// </summary>
    /// <remarks>
    /// 从LocatizationConfiguration读取LanguageInfo集合
    /// </remarks>
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
    {
        private readonly ILocalizationConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">本地化的配置</param>
        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <returns>语言信息列表</returns>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}