using System.Collections.Generic;

namespace Abp.Localization.Dictionaries.Json
{
    /// <summary>
    /// Use it to serialize json file
    /// Json本地化文件，
    /// 用于序列化Json文件。
    /// </summary>
    /// <remarks>
    /// 反序列化Json字符串到JsonLocalizationFile对象。
    /// </remarks>
    public class JsonLocalizationFile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JsonLocalizationFile()
        {
            Texts = new Dictionary<string, string>();
        }

        /// <summary>
        /// get or set the culture name; eg : en , en-us, zh-CN
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        ///  Key value pairs
        /// </summary>
        public Dictionary<string, string> Texts { get; private set; }
    }
}