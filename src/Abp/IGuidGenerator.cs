using System;

namespace Abp
{
    /// <summary>
    /// Used to generate Ids.
    /// Guid生成器接口
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// Creates a GUID.
        /// 创建一个Guid。
        /// </summary>
        Guid Create();
    }
}
