namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="UserNotificationInfoWithNotificationInfo"/>.
    /// 用户信息通知信息的通知扩展类。
    /// </summary>
    public static class UserNotificationInfoWithNotificationInfoExtensions
    {
        /// <summary>
        /// Converts <see cref="UserNotificationInfoWithNotificationInfo"/> to <see cref="UserNotification"/>.
        /// 转换<see cref="UserNotificationInfoWithNotificationInfo"/>到<see cref="UserNotification"/>。
        /// </summary>
        public static UserNotification ToUserNotification(this UserNotificationInfoWithNotificationInfo userNotificationInfoWithNotificationInfo)
        {
            return userNotificationInfoWithNotificationInfo.UserNotification.ToUserNotification(
                userNotificationInfoWithNotificationInfo.Notification.ToNotification()
                );
        }
    }
}