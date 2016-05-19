using System;

namespace Abp.Configuration
{
    /// <summary>
    /// Defines scope of a setting.
    /// 设置范围
    /// </summary>
    [Flags]
    public enum SettingScopes
    {
        /// <summary>
        /// Represents a setting that can be configured/changed for the application level.
        /// 应用程序范围，表示可配置/更改为应用程序级别的设置。
        /// </summary>
        Application = 1,

        /// <summary>
        /// Represents a setting that can be configured/changed for each Tenant.
        /// This is reserved
        /// 租户范围，表示可以为每个租户配置/更改的设置。
        /// </summary>
        Tenant = 2,

        /// <summary>
        /// Represents a setting that can be configured/changed for each User.
        /// 用户范围,表示可以为每个用户配置/更改的设置。
        /// </summary>
        User = 4
    }
}