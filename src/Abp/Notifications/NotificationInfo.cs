using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to store published/sent notification.
    /// 通知信息类，用于存储发布/发送的通知。
    /// </summary>
    [Serializable]
    [Table("AbpNotifications")]
    public class NotificationInfo : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// Indicated all tenant ids for <see cref="TenantIds"/> property.
        /// Value: "0".
        /// 全部租户Id，值："0"
        /// </summary>
        public const string AllTenantIds = "0";

        /// <summary>
        /// Maximum length of <see cref="NotificationName"/> property.
        /// Value: 128.
        /// 最大通知名称长度，值：128
        /// </summary>
        public const int MaxNotificationNameLength = 128;

        /// <summary>
        /// Maximum length of <see cref="Data"/> property.
        /// Value: 1048576 (1 MB).
        /// 最大数据长度，值：1048586（1 MB）
        /// </summary>
        public const int MaxDataLength = 1024 * 1024;

        /// <summary>
        /// Maximum lenght of <see cref="DataTypeName"/> property.
        /// Value: 512.
        /// 最大数据类型长度，值：512
        /// </summary>
        public const int MaxDataTypeNameLength = 512;

        /// <summary>
        /// Maximum lenght of <see cref="EntityTypeName"/> property.
        /// Value: 512.
        /// 最大实体类型名称长度，值：512
        /// </summary>
        public const int MaxEntityTypeNameLength = 256;

        /// <summary>
        /// Maximum lenght of <see cref="EntityTypeAssemblyQualifiedName"/> property.
        /// Value: 512.
        /// 最大实体类型程序集名称，值：512
        /// </summary>
        public const int MaxEntityTypeAssemblyQualifiedNameLength = 512;

        /// <summary>
        /// Maximum lenght of <see cref="EntityId"/> property.
        /// Value: 256.
        /// 最大实体长度：值：256
        /// </summary>
        public const int MaxEntityIdLength = 128;

        /// <summary>
        /// Maximum lenght of <see cref="UserIds"/> property.
        /// Value: 131072 (128 KB).
        /// 最大用户Ids长度，值：131072（128 KB）
        /// </summary>
        public const int MaxUserIdsLength = 128 * 1024;

        /// <summary>
        /// Maximum lenght of <see cref="TenantIds"/> property.
        /// Value: 131072 (128 KB).
        /// 最大租户Ids长度，值：131072（128 KB）
        /// </summary>
        public const int MaxTenantIdsLength = 128 * 1024;

        /// <summary>
        /// Unique notification name.
        /// 通知唯一名称
        /// </summary>
        [Required]
        [MaxLength(MaxNotificationNameLength)]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// Notification data as JSON string.
        /// 通知数据JSON字符串
        /// </summary>
        [MaxLength(MaxDataLength)]
        public virtual string Data { get; set; }

        /// <summary>
        /// Type of the JSON serialized <see cref="Data"/>.
        /// It's AssemblyQualifiedName of the type.
        /// 数据类型名称，
        /// JSON序列化的数据的类型。
        /// 这是该类型的组件名称。
        /// </summary>
        [MaxLength(MaxDataTypeNameLength)]
        public virtual string DataTypeName { get; set; }

        /// <summary>
        /// Gets/sets entity type name, if this is an entity level notification.
        /// It's FullName of the entity type.
        /// 获取/设置实体类型名称，如果这是一个实体级别的通知。
        /// 实体类型的全名称
        /// </summary>
        [MaxLength(MaxEntityTypeNameLength)]
        public virtual string EntityTypeName { get; set; }

        /// <summary>
        /// AssemblyQualifiedName of the entity type.
        /// 实体类型组件名称
        /// </summary>
        [MaxLength(MaxEntityTypeAssemblyQualifiedNameLength)]
        public virtual string EntityTypeAssemblyQualifiedName { get; set; }

        /// <summary>
        /// Gets/sets primary key of the entity, if this is an entity level notification.
        /// 获取/设置实体主键，如果这是一个实体级别的通知。
        /// </summary>
        [MaxLength(MaxEntityIdLength)]
        public virtual string EntityId { get; set; }

        /// <summary>
        /// Notification severity.
        /// 通知的严重程度。
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }

        /// <summary>
        /// Target users of the notification.
        /// If this is set, it overrides subscribed users.
        /// If this is null/empty, then notification is sent to all subscribed users.
        /// 目标用户的通知。
        /// 如果是这样，它将覆盖订阅用户。
        /// 如果这是空/空，然后通知发送到所有订阅用户。
        /// </summary>
        [MaxLength(MaxUserIdsLength)]
        public virtual string UserIds { get; set; }

        /// <summary>
        /// Excluded users.
        /// This can be set to exclude some users while publishing notifications to subscribed users.
        /// It's not normally used if <see cref="UserIds"/> is not null.
        /// 排除的用户。
        /// 这可以排除一些用户同时发布通知的订阅用户。
        /// 如果<see cref="UserIds"/>不为null，这不是通常用的。
        /// </summary>
        [MaxLength(MaxUserIdsLength)]
        public virtual string ExcludedUserIds { get; set; }

        /// <summary>
        /// Target tenants of the notification.
        /// Used to send notification to subscribed users of specific tenant(s).
        /// This is valid only if UserIds is null.
        /// If it's "0", then indicates to all tenants.
        /// 通知的目标租户。
        /// 用于发送通知给订阅用户特定的租户(s)。
        /// 如果用户名为null，这个值有效。
        /// 如果是“0”，则显示所有租户。
        /// </summary>
        [MaxLength(MaxTenantIdsLength)]
        public virtual string TenantIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationInfo"/> class.
        /// 初始化一个新的<see cref="NotificationInfo"/>通知信息实例
        /// </summary>
        public NotificationInfo()
        {
            Severity = NotificationSeverity.Info;
        }
    }
}