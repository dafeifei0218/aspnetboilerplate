using System.Reflection;

namespace Abp.Resources.Embedded
{
    /// <summary>
    /// Stores needed information of an embedded resource.
    /// 嵌入式资源信息
    /// </summary>
    public class EmbeddedResourceInfo
    {
        /// <summary>
        /// Content of the resource file.
        /// 资源文件内容
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// The assembly that contains the resource.
        /// 包含资源的程序集
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="content">资源文件内容</param>
        /// <param name="assembly">包含资源的程序集</param>
        internal EmbeddedResourceInfo(byte[] content, Assembly assembly)
        {
            Content = content;
            Assembly = assembly;
        }
    }
}