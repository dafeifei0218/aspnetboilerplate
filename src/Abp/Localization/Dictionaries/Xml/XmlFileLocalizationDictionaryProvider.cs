using System.IO;
using Abp.Localization.Sources.Xml;

namespace Abp.Localization.Dictionaries.Xml
{
    /// <summary>
    /// Provides localization dictionaries from XML files in a directory.
    /// Xml文件本地化字典提供者
    /// </summary>
    public class XmlFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly string _directoryPath;

        /// <summary>
        /// Creates a new <see cref="XmlFileLocalizationDictionaryProvider"/>.
        /// 构造函数
        /// </summary>
        /// <param name="directoryPath">Path of the dictionary that contains all related XML files</param>
        public XmlFileLocalizationDictionaryProvider(string directoryPath)
        {
            if (!Path.IsPathRooted(directoryPath))
            {
                directoryPath = Path.Combine(XmlLocalizationSource.RootDirectoryOfApplication, directoryPath);
            }

            _directoryPath = directoryPath;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sourceName">源名称</param>
        public override void Initialize(string sourceName)
        {
            var fileNames = Directory.GetFiles(_directoryPath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var fileName in fileNames)
            {
                var dictionary = CreateXmlLocalizationDictionary(fileName);
                if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                {
                    throw new AbpInitializationException(sourceName + " source contains more than one dictionary for the culture: " + dictionary.CultureInfo.Name);
                }

                Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                if (fileName.EndsWith(sourceName + ".xml"))
                {
                    if (DefaultDictionary != null)
                    {
                        throw new AbpInitializationException("Only one default localization dictionary can be for source: " + sourceName);                        
                    }

                    DefaultDictionary = dictionary;
                }
            }
        }

        /// <summary>
        /// 创建Xml本地化字典
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string fileName)
        {
            return XmlLocalizationDictionary.BuildFomFile(fileName);
        }
    }
}