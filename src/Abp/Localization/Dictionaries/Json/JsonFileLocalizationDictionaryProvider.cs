﻿using System.IO;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources.Xml;

namespace Abp.Localization.Dictionaries.Json
{
    /// <summary>
    /// Provides localization dictionaries from json files in a directory.
    /// </summary>
    /// <remarks>
    /// 提供从Json文件中读取本地化信息，并将本地化信息装载到DefaultDictionary（IDictionary<string, ILocalizationDictionary>对象）中。
    /// </remarks>
    public class JsonFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly string _directoryPath;

        /// <summary>
        ///     Creates a new <see cref="JsonFileLocalizationDictionaryProvider" />.
        /// </summary>
        /// <param name="directoryPath">Path of the dictionary that contains all related XML files</param>
        public JsonFileLocalizationDictionaryProvider(string directoryPath)
        {
            if (!Path.IsPathRooted(directoryPath))
            {
                directoryPath = Path.Combine(XmlLocalizationSource.RootDirectoryOfApplication, directoryPath);
            }

            _directoryPath = directoryPath;
        }
        
        public override void Initialize(string sourceName)
        {
            var fileNames = Directory.GetFiles(_directoryPath, "*.json", SearchOption.TopDirectoryOnly);

            foreach (var fileName in fileNames)
            {
                var dictionary = CreateJsonLocalizationDictionary(fileName);
                if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                {
                    throw new AbpInitializationException(sourceName + " source contains more than one dictionary for the culture: " + dictionary.CultureInfo.Name);
                }

                Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                if (fileName.EndsWith(sourceName + ".json"))
                {
                    if (DefaultDictionary != null)
                    {
                        throw new AbpInitializationException("Only one default localization dictionary can be for source: " + sourceName);
                    }

                    DefaultDictionary = dictionary;
                }
            }
        }

        protected virtual JsonLocalizationDictionary CreateJsonLocalizationDictionary(string fileName)
        {
            return JsonLocalizationDictionary.BuildFromFile(fileName);
        }
    }
}