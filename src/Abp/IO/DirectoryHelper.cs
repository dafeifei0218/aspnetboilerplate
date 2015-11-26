using System.IO;

namespace Abp.IO
{
    /// <summary>
    /// A helper class for Directory operations.
    /// 目录操作帮助类
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Creates a new directory if it does not exists.
        /// 创建目录，如果目录不存在
        /// </summary>
        /// <param name="directory">Directory to create 创建目录</param>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}