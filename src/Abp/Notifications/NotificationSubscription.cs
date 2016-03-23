using System;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Abp.Notifications
{
    /// <summary>
    /// Represents a user subscription to a notification.
    /// 通知订阅，表示一个用户的通知订阅。
    /// </summary>
    public class NotificationSubscription : IHasCreationTime
    {
        /// <summary>
        /// User Id.
        /// 用户Id。
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Notification unique name.
        /// 通知的唯一名称。
        /// </summary>
        public string NotificationName { get; set; }

        /// <summary>
        /// Entity type.
        /// 通知类型。
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Name of the entity type (including namespaces).
        /// 通知类型名称（包含命名空间）。
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity Id.
        /// 实体Id。
        /// </summary>
        public object EntityId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscription"/> class.
        /// 初始化一个新的<see cref="NotificationSubscription"/>通知订阅实例
        /// </summary>
        public NotificationSubscription()
        {
            CreationTime = Clock.Now;
        }
    }
}