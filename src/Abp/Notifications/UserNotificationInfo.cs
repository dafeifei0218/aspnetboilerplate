using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to store a user notification.
    /// 用户通知信息，用于用户通知信息。
    /// </summary>
    [Serializable]
    [Table("AbpUserNotifications")]
    public class UserNotificationInfo : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// User Id.
        /// 用户Id。
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Notification Id.
        /// 通知Id。
        /// </summary>
        [Required]
        public virtual Guid NotificationId { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// 当前用户通知状态。
        /// </summary>
        public virtual UserNotificationState State { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationInfo"/> class.
        /// 实例化一个新的<see cref="UserNotificationInfo"/>用户通知信息类
        /// </summary>
        public UserNotificationInfo()
        {
            State = UserNotificationState.Unread;
            CreationTime = Clock.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationInfo"/> class.
        /// 实例化一个新的<see cref="UserNotificationInfo"/>用户通知信息类
        /// </summary>
        public UserNotificationInfo(long userId, Guid notificationId)
            : this()
        {
            UserId = userId;
            NotificationId = notificationId;
        }
    }
}