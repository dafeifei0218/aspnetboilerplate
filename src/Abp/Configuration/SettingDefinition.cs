using System;
using Abp.Localization;

namespace Abp.Configuration
{
    /// <summary>
    /// Defines a setting.
    /// A setting is used to configure and change behavior of the application.
    /// 设置定义。
    /// 设置用于配置和更改应用程序的行为。
    /// </summary>
    public class SettingDefinition
    {
        /// <summary>
        /// Unique name of the setting.
        /// 设置名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the setting.
        /// This can be used to show setting to the user.
        /// 设置显示名称
        /// </summary>
        public ILocalizableString DisplayName { get; private set; }

        /// <summary>
        /// A brief description for this setting.
        /// 设置描述
        /// </summary>
        public ILocalizableString Description { get; private set; }

        /// <summary>
        /// Scopes of this setting.
        /// Default value: <see cref="SettingScopes.Application"/>.
        /// 设置范围
        /// </summary>
        public SettingScopes Scopes { get; private set; }

        /// <summary>
        /// Is this setting inherited from parent scopes.
        /// Default: True.
        /// 是否继承，默认值：true
        /// </summary>
        public bool IsInherited { get; set; }

        /// <summary>
        /// Gets/sets group for this setting.
        /// 设置定义分组
        /// </summary>
        public SettingDefinitionGroup Group { get; private set; }

        /// <summary>
        /// Default value of the setting.
        /// 设置默认值
        /// </summary>
        public string DefaultValue { get; private set; }

        /// <summary>
        /// Can clients see this setting and it's value.
        /// It maybe dangerous for some settings to be visible to clients (such as email server password).
        /// Default: false.
        /// </summary>
        public bool IsVisibleToClients { get; private set; }

        /// <summary>
        /// Can be used to store a custom object related to this setting.
        /// 自定义数据
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Creates a new <see cref="SettingDefinition"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="defaultValue">Default value of the setting 默认设置值</param>
        /// <param name="displayName">Display name of the permission 显示名称</param>
        /// <param name="group">Group of this setting 设置组</param>
        /// <param name="description">A brief description for this setting 设置描述</param>
        /// <param name="scopes">Scopes of this setting. Default value: <see cref="SettingScopes.Application"/>. 设置作用域</param>
        /// <param name="isVisibleToClients">Can clients see this setting and it's value. Default: false 客户端是否可见，默认值：false</param>
        /// <param name="isInherited">Is this setting inherited from parent scopes. Default: True. 是否从父域继承，默认值：tue</param>
        /// <param name="customData">Can be used to store a custom object related to this setting 自定义数据</param>
        public SettingDefinition(
            string name, 
            string defaultValue, 
            ILocalizableString displayName = null, 
            SettingDefinitionGroup group = null, 
            ILocalizableString description = null, 
            SettingScopes scopes = SettingScopes.Application, 
            bool isVisibleToClients = false, 
            bool isInherited = true,
            object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            DefaultValue = defaultValue;
            DisplayName = displayName;
            Group = @group;
            Description = description;
            Scopes = scopes;
            IsVisibleToClients = isVisibleToClients;
            IsInherited = isInherited;
            CustomData = customData;
        }
    }
}
