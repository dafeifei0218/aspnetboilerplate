namespace Abp.Notifications
{
    /// <summary>
    /// A class contains a <see cref="UserNotificationInfo"/> and related <see cref="NotificationInfo"/>.
    /// 用户信息通知信息的通知。
    /// </summary>
    public class UserNotificationInfoWithNotificationInfo
    {
        /// <summary>
        /// User notification.
        /// 用户通知信息。
        /// </summary>
        public UserNotificationInfo UserNotification { get; set; }

        /// <summary>
        /// Notification.
        /// 通知信息。
        /// </summary>
        public NotificationInfo Notification { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationInfoWithNotificationInfo"/> class.
        /// 初始化的一个新实例<see cref="UserNotificationInfoWithNotificationInfo"/>用户信息通知信息的通知。
        /// </summary>
        public UserNotificationInfoWithNotificationInfo(UserNotificationInfo userNotification, NotificationInfo notification)
        {
            UserNotification = userNotification;
            Notification = notification;
        }
    }
}