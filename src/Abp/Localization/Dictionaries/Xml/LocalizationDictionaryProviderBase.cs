using System.Collections.Generic;

namespace Abp.Localization.Dictionaries.Xml
{
    /// <summary>
    /// 本地化字典提供者基类
    /// </summary>
    public abstract class LocalizationDictionaryProviderBase : ILocalizationDictionaryProvider
    {
        /// <summary>
        /// 本地化源名称
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        /// 默认本地化字典
        /// </summary>
        public ILocalizationDictionary DefaultDictionary { get; protected set; }

        /// <summary>
        /// 字典
        /// </summary>
        public IDictionary<string, ILocalizationDictionary> Dictionaries { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected LocalizationDictionaryProviderBase()
        {
            Dictionaries = new Dictionary<string, ILocalizationDictionary>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sourceName">本地化源名称</param>
        public virtual void Initialize(string sourceName)
        {
            SourceName = sourceName;
        }

        /// <summary>
        /// 扩展
        /// </summary>
        /// <param name="dictionary">本地化字典</param>
        public void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            //添加
            ILocalizationDictionary existingDictionary;
            if (!Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                Dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //Override
            //重写
            var localizedStrings = dictionary.GetAllStrings();
            foreach (var localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }
}