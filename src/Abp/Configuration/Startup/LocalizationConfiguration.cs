using System.Collections.Generic;
using Abp.Localization;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used for localization configurations.
    /// 本地化配置
    /// </summary>
    internal class LocalizationConfiguration : ILocalizationConfiguration
    {
        /// <summary>
        /// 语言信息列表
        /// </summary>
        /// <inheritdoc/>
        public IList<LanguageInfo> Languages { get; private set; }

        /// <summary>
        /// 本地资源列表
        /// </summary>
        /// <inheritdoc/>
        public ILocalizationSourceList Sources { get; private set; }

        /// <summary>
        /// 是否启用，默认值：true
        /// </summary>
        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 如果未找到，返回给定的文本
        /// 如果设置为true，则返回给定的文本（名称）在定位源中未找到。
        /// 防止例外在本地化源中没有定义给定名称。
        /// 也写一个警告日志。
        /// 默认值：true
        /// </summary>
        /// <inheritdoc/>
        public bool ReturnGivenTextIfNotFound { get; set; }

        /// <summary>
        /// 如果没有找到包装文本
        /// 他返回给定的文本用以[and]字符在定位源中未找到。
        /// 只考虑如果ReturnGivenTextIfNotFound为true
        /// 默认值：true
        /// </summary>
        /// <inheritdoc/>
        public bool WrapGivenTextIfNotFound { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LocalizationConfiguration()
        {
            Languages = new List<LanguageInfo>();
            Sources = new LocalizationSourceList();

            IsEnabled = true;
            ReturnGivenTextIfNotFound = true;
            WrapGivenTextIfNotFound = true;
        }
    }
}