using System.Collections.Generic;
using Abp.Localization.Sources;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// A specialized list to store <see cref="ILocalizationSource"/> object.
    /// 本地化资源列表
    /// </summary>
    internal class LocalizationSourceList : List<ILocalizationSource>, ILocalizationSourceList
    {
        /// <summary>
        /// 本地化源列表扩展信息列表
        /// </summary>
        public IList<LocalizationSourceExtensionInfo> Extensions { get; private set; }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public LocalizationSourceList()
        {
            Extensions = new List<LocalizationSourceExtensionInfo>();
        }
    }
}