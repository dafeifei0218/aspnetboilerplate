using System;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// Represents sides in a multi tenancy application.
    /// 多租户双方，表示多租户应用程序中的双方。
    /// </summary>
    [Flags]
    public enum MultiTenancySides
    {
        /// <summary>
        /// Tenant side.
        /// 租户方
        /// </summary>
        Tenant = 1,
        
        /// <summary>
        /// Host (tenancy owner) side.
        /// Host（租户拥有者）方
        /// </summary>
        Host = 2
    }
}