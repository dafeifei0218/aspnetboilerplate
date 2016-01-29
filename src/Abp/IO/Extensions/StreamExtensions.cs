using System.IO;

namespace Abp.IO.Extensions
{
    /// <summary>
    /// Stream扩展类
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 获取全部字节
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>字节数组</returns>
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
