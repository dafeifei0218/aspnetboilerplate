using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Abp.Notifications
{
    /// <summary>
    /// Represents a published notification.
    /// 通知，
    /// </summary> 
    /// <remarks>
    /// 用于封装Notification 的信息。用作DTO而不是Entity
    /// </remarks>
    [Serializable]
    public class Notification : EntityDto<Guid>, IHasCreationTime
    {
        /// <summary>
        /// Unique notification name.
        /// 通知唯一名称
        /// </summary>
        public string NotificationName { get; set; }

        /// <summary>
        /// Notification data.
        /// 通知数据
        /// </summary>
        public NotificationData Data { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// 获取或设置实体类型
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Name of the entity type (including namespaces).
        /// 实体类型名称（包括命名空间）。
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity id.
        /// 实体Id
        /// </summary>
        public object EntityId { get; set; }

        /// <summary>
        /// Severity.
        /// 通知严重程度
        /// </summary>
        public NotificationSeverity Severity { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// 实例化一个新的<see cref="Notification"/>通知类。
        /// </summary>
        public Notification()
        {
            CreationTime = Clock.Now;
        }
    }
}