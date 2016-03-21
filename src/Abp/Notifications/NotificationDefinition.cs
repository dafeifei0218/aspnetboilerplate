using System;
using System.Collections.Generic;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Localization;

namespace Abp.Notifications
{
    /// <summary>
    /// Definition for a notification.
    /// 通知的定义
    /// </summary>
    public class NotificationDefinition
    {
        /// <summary>
        /// Unique name of the notification.
        /// 通知的唯一名称。
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Related entity type with this notification (optional).
        /// 相关实体类型与此通知（可选）。
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// Display name of the notification.
        /// Optional.
        /// 通知显示名称。可选。
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// Description for the notification.
        /// Optional.
        /// 通知说明。可选。
        /// </summary>
        public ILocalizableString Description { get; set; }

        /// <summary>
        /// A permission dependency. This notification will be available to a user if this dependency is satisfied.
        /// Optional.
        /// 权限依赖，
        /// 如果这个依赖满足，该通知将提供给用户。可选。
        /// </summary>
        public IPermissionDependency PermissionDependency { get; set; }
        
        /// <summary>
        /// A feature dependency. This notification will be available to a tenant if this feature is enabled.
        /// Optional.
        /// 特征依赖，
        /// 如果启用此功能，该通知将提供给租户。可选。
        /// </summary>
        public IFeatureDependency FeatureDependency { get; set; }

        /// <summary>
        /// Gets/sets arbitrary objects related to this object.
        /// Gets null if given key does not exists.
        /// This is a shortcut for <see cref="Attributes"/> dictionary.
        /// 获取/设置与此对象相关的任意对象。
        /// 如果给定密钥不存在，就会变为空。
        /// 这是一个简短的<see cref="Attributes"/>字典。
        /// </summary>
        /// <param name="key">Key 键</param>
        public object this[string key]
        {
            get { return Attributes.GetOrDefault(key); }
            set { Attributes[key] = value; }
        }

        /// <summary>
        /// Arbitrary objects related to this object.
        /// These objects must be serializable.
        /// 属性字典，
        /// 与此对象相关的任意对象。
        /// 这是对象必须是可序列化。
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDefinition"/> class.
        /// 初始化一个新实例<see cref="NotificationDefinition"/>通知定义类
        /// </summary>
        /// <param name="name">Unique name of the notification. 通知的唯一名称。</param>
        /// <param name="entityType">Related entity type with this notification (optional).</param>
        /// <param name="displayName">Display name of the notification. 通知显示名称</param>
        /// <param name="description">Description for the notification 通知说明</param>
        /// <param name="permissionDependency">A permission dependency. This notification will be available to a user if this dependency is satisfied. 一个权限依赖，如果这个依赖满足，该通知将提供给用户。可选。</param>
        /// <param name="featureDependency">A feature dependency. This notification will be available to a tenant if this feature is enabled. 一个特征依赖，如果启用此功能，该通知将提供给租户。可选。</param>
        public NotificationDefinition(string name, Type entityType = null, ILocalizableString displayName = null, ILocalizableString description = null, IPermissionDependency permissionDependency = null, IFeatureDependency featureDependency = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "name can not be null, empty or whitespace!");
            }
            
            Name = name;
            EntityType = entityType;
            DisplayName = displayName;
            Description = description;
            PermissionDependency = permissionDependency;
            FeatureDependency = featureDependency;

            Attributes = new Dictionary<string, object>();
        }
    }
}
