using System.IO;

namespace Abp.IO
{
    /// <summary>
    /// A helper class for File operations.
    /// 文件帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Checks and deletes given file if it does exists.
        /// 删除文件时检查文件是否存在
        /// </summary>
        /// <param name="filePath">Path of the file 文件路径</param>
        public static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
