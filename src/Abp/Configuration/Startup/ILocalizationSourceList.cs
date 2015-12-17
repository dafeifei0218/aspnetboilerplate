using System.Collections.Generic;
using Abp.Localization.Sources;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Defines a specialized list to store <see cref="ILocalizationSource"/> object.
    /// 本地化源列表接口
    /// </summary>
    public interface ILocalizationSourceList : IList<ILocalizationSource>
    {
        /// <summary>
        /// Extensions for dictionay based localization sources.
        /// 本地化源列表扩展信息列表
        /// </summary>
        IList<LocalizationSourceExtensionInfo> Extensions { get; }
    }
}