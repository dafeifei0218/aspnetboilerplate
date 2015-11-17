using System;

namespace Abp.AutoMapper
{
    /// <summary>
    /// AutoMap方向枚举
    /// </summary>
    [Flags]
    public enum AutoMapDirection
    {
        /// <summary>
        /// 从
        /// </summary>
        From,

        /// <summary>
        /// 向
        /// </summary>
        To
    }
}