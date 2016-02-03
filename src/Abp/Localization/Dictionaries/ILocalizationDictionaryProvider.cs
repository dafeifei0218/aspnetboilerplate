using System.Collections.Generic;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Used to get localization dictionaries (<see cref="ILocalizationDictionary"/>)
    /// for a <see cref="IDictionaryBasedLocalizationSource"/>.
    /// 本地化字典提供者接口
    /// </summary>
    public interface ILocalizationDictionaryProvider
    {
        /// <summary>
        /// 默认字典 
        /// </summary>
        ILocalizationDictionary DefaultDictionary { get; }

        /// <summary>
        /// 字典
        /// </summary>
        IDictionary<string, ILocalizationDictionary> Dictionaries { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sourceName">源名称</param>
        void Initialize(string sourceName);
        
        /// <summary>
        /// 扩展
        /// </summary>
        /// <param name="dictionary">本地化字典</param>
        void Extend(ILocalizationDictionary dictionary);
    }
}