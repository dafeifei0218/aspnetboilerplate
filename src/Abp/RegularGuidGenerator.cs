using System;
using Abp.Dependency;

namespace Abp
{
    /// <summary>
    /// Implements <see cref="IGuidGenerator"/> by using <see cref="Guid.NewGuid"/>.
    /// 定期Guid生成器，实现<see cref="IGuidGenerator"/>
    /// </summary>
    public class RegularGuidGenerator : IGuidGenerator, ITransientDependency
    {
        /// <summary>
        /// 创建一个Guid。
        /// </summary>
        /// <returns></returns>
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}