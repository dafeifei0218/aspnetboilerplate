using System;
using System.IO;
using System.Reflection;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;

namespace Abp.Localization.Sources.Xml
{
    /// <summary>
    /// XML based localization source.
    /// It uses XML files to read localized strings.
    /// Xml本地化源，使用XML文件读取本地化的字符串。
    /// 直接使用DictionaryBasedLocalizationSource和XmlFileLocalizationDictionaryProvider代替
    /// </summary>
    [Obsolete("Directly use DictionaryBasedLocalizationSource with XmlFileLocalizationDictionaryProvider instead of this class")]
    public class XmlLocalizationSource : DictionaryBasedLocalizationSource, ISingletonDependency
    {
        /// <summary>
        /// 应用根目录，找到更好的方式通过根目录
        /// </summary>
        internal static string RootDirectoryOfApplication { get; set; } //TODO: Find a better way of passing root directory

        /// <summary>
        /// 构造函数
        /// </summary>
        static XmlLocalizationSource()
        {
            RootDirectoryOfApplication = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Creates an Xml based localization source.
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique Name of the source 本地化源名称</param>
        /// <param name="directoryPath">Directory path of the localization XML files 本地化文件的目录路径</param>
        public XmlLocalizationSource(string name, string directoryPath)
            : base(name, new XmlFileLocalizationDictionaryProvider(directoryPath))
        {

        }
    }
}
