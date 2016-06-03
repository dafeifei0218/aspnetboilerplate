using System.Collections.Generic;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Used to get localization dictionaries (<see cref="ILocalizationDictionary"/>)
    /// for a <see cref="IDictionaryBasedLocalizationSource"/>.
    /// 本地化字典提供者接口
    /// </summary>
    /// <remarks>
    /// 它封装了一个IDictionary<string, ILocalizationDictionary>实例(这是ABP在runtime时候，唯一持有本地化资源的对象)，
    /// 其中key就是sourceName(比如上面的"SimpleTaskSystem")。
    /// 并且提供了一个方法Initialize来初始化本地化这个Dictionary。
    /// 可以通过实现这个接口来提供其他类型的本地化资源。
    /// 比如Abp.Zero 就实现了数据库的本地化资源。
    /// </remarks>
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