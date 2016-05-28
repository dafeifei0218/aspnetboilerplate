using System.Reflection;
using System.Text;
using Abp.IO.Extensions;

namespace Abp.Localization.Dictionaries.Xml
{
    /// <summary>
    /// Provides localization dictionaries from XML files embedded into an <see cref="Assembly"/>.
    /// Xml嵌入文件本地化字典提供者
    /// </summary>
    /// <remarks>
    /// 提供从Json文件（本地资源）中读取本地化信息，并将本地化信息装载到DefaultDictionary（IDictionary对象）中。
    /// </remarks>
    public class XmlEmbeddedFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly Assembly _assembly;
        private readonly string _rootNamespace;
        
        /// <summary>
        /// Creates a new <see cref="XmlEmbeddedFileLocalizationDictionaryProvider"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="assembly">Assembly that contains embedded xml files </param>
        /// <param name="rootNamespace">Namespace of the embedded xml dictionary files</param>
        public XmlEmbeddedFileLocalizationDictionaryProvider(Assembly assembly, string rootNamespace)
        {
            _assembly = assembly;
            _rootNamespace = rootNamespace;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sourceName">源名称</param>
        public override void Initialize(string sourceName)
        {
            var resourceNames = _assembly.GetManifestResourceNames();
            foreach (var resourceName in resourceNames)
            {
                if (resourceName.StartsWith(_rootNamespace))
                {
                    using (var stream = _assembly.GetManifestResourceStream(resourceName))
                    {
                        var bytes = stream.GetAllBytes();
                        var xmlString = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3); //Skipping byte order mark

                        var dictionary = CreateXmlLocalizationDictionary(xmlString);
                        if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                        {
                            throw new AbpInitializationException(sourceName + " source contains more than one dictionary for the culture: " + dictionary.CultureInfo.Name);
                        }

                        Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                        if (resourceName.EndsWith(sourceName + ".xml"))
                        {
                            if (DefaultDictionary != null)
                            {
                                throw new AbpInitializationException("Only one default localization dictionary can be for source: " + sourceName);
                            }

                            DefaultDictionary = dictionary;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 创建Xml本地化字典
        /// </summary>
        /// <param name="xmlString">xml字符串</param>
        /// <returns></returns>
        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string xmlString)
        {
            return XmlLocalizationDictionary.BuildFomXmlString(xmlString);
        }
    }
}