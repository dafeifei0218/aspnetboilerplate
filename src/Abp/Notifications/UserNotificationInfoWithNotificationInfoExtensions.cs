namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="UserNotificationInfoWithNotificationInfo"/>.
    /// �û���Ϣ֪ͨ��Ϣ��֪ͨ��չ�ࡣ
    /// </summary>
    public static class UserNotificationInfoWithNotificationInfoExtensions
    {
        /// <summary>
        /// Converts <see cref="UserNotificationInfoWithNotificationInfo"/> to <see cref="UserNotification"/>.
        /// ת��<see cref="UserNotificationInfoWithNotificationInfo"/>��<see cref="UserNotification"/>��
        /// </summary>
        public static UserNotification ToUserNotification(this UserNotificationInfoWithNotificationInfo userNotificationInfoWithNotificationInfo)
        {
            return userNotificationInfoWithNotificationInfo.UserNotification.ToUserNotification(
                userNotificationInfoWithNotificationInfo.Notification.ToNotification()
                );
        }
    }
}