namespace Abp.Notifications
{
    /// <summary>
    /// Pre-defined setting names for notification system.
    /// 通知系统设置名称
    /// </summary>
    public static class NotificationSettingNames
    {
        /// <summary>
        /// A top-level switch to enable/disable receiving notifications for a user.
        /// "Abp.Notifications.ReceiveNotifications".
        /// 收到通知，
        /// 一个顶级的开关来启用/禁用用户接收通知。
        /// "Abp.Notifications.ReceiveNotifications".
        /// </summary>
        public const string ReceiveNotifications = "Abp.Notifications.ReceiveNotifications";
    }
}