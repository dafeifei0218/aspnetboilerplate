using System.Reflection;

namespace Abp.Resources.Embedded
{
    /// <summary>
    /// Provides infrastructure to use any type of resources (files) embedded into assemblies.
    /// 嵌入式资源管理类
    /// </summary>
    public interface IEmbeddedResourceManager
    {
        /// <summary>
        /// Makes possible to expose all files in a folder (and subfolders recursively).
        /// 暴露资源，可以将所有的文件在一个文件夹（及其子文件夹递归）。
        /// </summary>
        /// <param name="rootPath">
        /// Root folder path to be seen by clients.
        /// This is an arbitrary value with any deep.
        /// 根目录，
        /// 客户端可看到的根文件夹路径。
        /// 这是一个任意值与任何深。
        /// </param>
        /// <param name="assembly">The assembly contains resources. 程序集包含的资源</param>
        /// <param name="resourceNamespace">Namespace in the <paramref name="assembly"/> that matches to the root path 资源命名空间，在与根路径匹配的程序集中的命名空间</param>
        void ExposeResources(string rootPath, Assembly assembly, string resourceNamespace);

        /// <summary>
        /// Used to get an embedded resource file.
        /// 获取嵌入资源文件
        /// </summary>
        /// <param name="fullResourcePath">Full path of the resource 资源全路径</param>
        /// <returns>The resource 嵌入式资源信息</returns>
        EmbeddedResourceInfo GetResource(string fullResourcePath);
    }
}