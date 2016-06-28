using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Json;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to store a notification subscription.
    /// 通知订阅的信息
    /// </summary>
    /// <remarks>
    /// 用于封装Notification 通知 和Subscription 订阅人 的关系的Entity
    /// </remarks>
    [Table("AbpNotificationSubscriptions")]
    public class NotificationSubscriptionInfo : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// Tenant id of the subscribed user.
        /// Note: This class does not implement <see cref="IMayHaveTenant"/> filter.
        /// So, it should be manually filtered if needed.
        /// 订阅用户的租户Id。
        /// 注意：这个类不实现<see cref="IMayHaveTenant"/>过滤。
        /// 因此如果需要的话，他应该手动过滤。
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// User Id.
        /// 用户Id
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Notification unique name.
        /// 通知唯一名称
        /// </summary>
        [MaxLength(NotificationInfo.MaxNotificationNameLength)]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// Gets/sets entity type name, if this is an entity level notification.
        /// It's FullName of the entity type.
        /// 获取/设置实体类型名称，如果这是一个实体级别的通知。
        /// 这是实体类型的全名称。
        /// </summary>
        [MaxLength(NotificationInfo.MaxEntityTypeNameLength)]
        public virtual string EntityTypeName { get; set; }

        /// <summary>
        /// AssemblyQualifiedName of the entity type.
        /// 实体类型的程序集名称。
        /// </summary>
        [MaxLength(NotificationInfo.MaxEntityTypeAssemblyQualifiedNameLength)]
        public virtual string EntityTypeAssemblyQualifiedName { get; set; }

        /// <summary>
        /// Gets/sets primary key of the entity, if this is an entity level notification.
        /// 获取/设置实体主键，如果这是一个实体级别的通知。
        /// </summary>
        [MaxLength(NotificationInfo.MaxEntityIdLength)]
        public virtual string EntityId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscriptionInfo"/> class.
        /// 初始化一个新的<see cref="NotificationSubscriptionInfo"/>通知订阅的信息类
        /// </summary>
        public NotificationSubscriptionInfo()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscriptionInfo"/> class.
        /// 初始化一个新的<see cref="NotificationSubscriptionInfo"/>通知订阅的信息类
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityIdentifier">实体标识</param>
        public NotificationSubscriptionInfo(int? tenantId, long userId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            TenantId = tenantId;
            NotificationName = notificationName;
            UserId = userId;
            EntityTypeName = entityIdentifier == null ? null : entityIdentifier.Type.FullName;
            EntityTypeAssemblyQualifiedName = entityIdentifier == null ? null : entityIdentifier.Type.AssemblyQualifiedName;
            EntityId = entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString();
        }
    }
}