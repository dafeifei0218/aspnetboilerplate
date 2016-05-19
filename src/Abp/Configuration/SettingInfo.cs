﻿using System;

namespace Abp.Configuration
{
    /// <summary>
    /// Represents a setting information.
    /// 设置信息
    /// </summary>
    [Serializable]
    public class SettingInfo
    {
        /// <summary>
        /// TenantId for this setting.
        /// TenantId is null if this setting is not Tenant level.
        /// 设置的租户Id。
        /// 如果设置不为租户级别，租户Id为空。
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// UserId for this setting.
        /// UserId is null if this setting is not user level.
        /// 设置的用户Id
        /// 如果设置不为用户级别，用户Id为空。
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Unique name of the setting.
        /// 设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the setting.
        /// 设置的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="SettingInfo"/> object.
        /// 构造函数
        /// </summary>
        public SettingInfo()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="SettingInfo"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">TenantId for this setting. TenantId is null if this setting is not Tenant level. 租户Id</param>
        /// <param name="userId">UserId for this setting. UserId is null if this setting is not user level. 用户Id</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="value">Value of the setting 设置的值</param>
        public SettingInfo(int? tenantId, long? userId, string name, string value)
        {
            TenantId = tenantId;
            UserId = userId;
            Name = name;
            Value = value;
        }
    }
}