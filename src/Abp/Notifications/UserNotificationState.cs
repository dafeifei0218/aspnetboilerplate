namespace Abp.Notifications
{
    /// <summary>
    /// Represents state of a <see cref="UserNotification"/>.
    /// 用户通知状态。
    /// </summary>
    /// <remarks>
    /// 表示UserNotification的状态：读和未读
    /// </remarks>
    public enum UserNotificationState
    {
        /// <summary>
        /// Notification is not read by user yet.
        /// 未读，用户未读取通知。
        /// </summary>
        Unread = 0,

        /// <summary>
        /// Notification is read by user.
        /// 已读，用户已读取通知。
        /// </summary>
        Read
    }
}