using System.IO;

namespace Abp.IO
{
    /// <summary>
    /// A helper class for Directory operations.
    /// Ŀ¼����������
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Creates a new directory if it does not exists.
        /// ����Ŀ¼�����Ŀ¼������
        /// </summary>
        /// <param name="directory">Directory to create ����Ŀ¼</param>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}