using System.Collections.Generic;
using Abp.Localization;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used for localization configurations.
    /// 本地化配置接口
    /// </summary>
    public interface ILocalizationConfiguration
    {
        /// <summary>
        /// Used to set languages available for this application.
        /// 语言信息列表
        /// </summary>
        IList<LanguageInfo> Languages { get; }

        /// <summary>
        /// List of localization sources.
        /// 本地资源列表
        /// </summary>
        ILocalizationSourceList Sources { get; }

        /// <summary>
        /// Used to enable/disable localization system.
        /// Default: true.
        /// 是否启用，默认值：true
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// If this is set to true, the given text (name) is returned
        /// if not found in the localization source. That prevent exceptions if
        /// given name is not defined in the localization sources.
        /// Also writes a warning log.
        /// Default: true.
        /// 如果未找到，返回给定的文本
        /// 如果设置为true，则返回给定的文本（名称）在定位源中未找到。
        /// 防止例外在本地化源中没有定义给定名称。
        /// 也写一个警告日志。
        /// 默认值：true
        /// </summary>
        bool ReturnGivenTextIfNotFound { get; set; }

        /// <summary>
        /// It returns the given text by wrapping with [ and ] chars
        /// if not found in the localization source.
        /// This is considered only if <see cref="ReturnGivenTextIfNotFound"/> is true.
        /// Default: true.
        /// 如果没有找到包装文本
        /// 他返回给定的文本用以[and]字符在定位源中未找到。
        /// 只考虑如果ReturnGivenTextIfNotFound为true
        /// 默认值：true
        /// </summary>
        bool WrapGivenTextIfNotFound { get; set; }
    }
}