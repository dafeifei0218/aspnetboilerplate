using System;

namespace Abp.Application.Features
{
    /// <summary>
    /// Scopes of a <see cref="Feature"/>.
    /// 功能范围
    /// </summary>
    [Flags]
    public enum FeatureScopes
    {
        /// <summary>
        /// This <see cref="Feature"/> can be enabled/disabled per edition.
        /// 此功能可启用/禁用每个版本
        /// </summary>
        Edition = 1,

        /// <summary>
        /// This Feature<see cref="Feature"/> can be enabled/disabled per tenant.
        /// 此功能可启用/禁用每个租户
        /// </summary>
        Tenant = 2,

        /// <summary>
        /// This <see cref="Feature"/> can be enabled/disabled per tenant and edition.
        /// 此功能可启用/禁用每个租户和版本。
        /// </summary>
        All = 3
    }
}