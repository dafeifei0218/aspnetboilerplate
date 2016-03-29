namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="UserNotificationInfo"/>.
    /// 用户通知信息扩展类
    /// </summary>
    public static class UserNotificationInfoExtensions
    {
        /// <summary>
        /// Converts <see cref="UserNotificationInfo"/> to <see cref="UserNotification"/>.
        /// 转换<see cref="UserNotificationInfo"/>用户通知信息到<see cref="UserNotification"/>用户通知。
        /// </summary>
        public static UserNotification ToUserNotification(this UserNotificationInfo userNotificationInfo, Notification notification)
        {
            return new UserNotification
            {
                Id = userNotificationInfo.Id,
                Notification = notification,
                UserId = userNotificationInfo.UserId,
                State = userNotificationInfo.State
            };
        }
    }
}