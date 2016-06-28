using System;
using Abp.Application.Services.Dto;

namespace Abp.Notifications
{
    /// <summary>
    /// Represents a notification sent to a user.
    /// 用户通知，表示发送给用户的通知。
    /// </summary>
    /// <remarks>
    /// 用于封装User和Notification关系的信息。用作DTO而不是Entity
    /// </remarks>
    public class UserNotification : EntityDto<Guid>
    {
        /// <summary>
        /// User Id.
        /// 用户Id。
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// 当前用户通知的状态。
        /// </summary>
        public UserNotificationState State { get; set; }

        /// <summary>
        /// The notification.
        /// 用户通知
        /// </summary>
        public Notification Notification { get; set; }
    }
}