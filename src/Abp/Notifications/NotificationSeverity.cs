namespace Abp.Notifications
{
    /// <summary>
    /// Notification severity.
    /// 通知严重程度。
    /// </summary>
    public enum NotificationSeverity : byte
    {
        /// <summary>
        /// Info.
        /// 信息。
        /// </summary>
        Info = 0,
        
        /// <summary>
        /// Success.
        /// 成功。
        /// </summary>
        Success = 1,
        
        /// <summary>
        /// Warn.
        /// 警告。
        /// </summary>
        Warn = 2,
        
        /// <summary>
        /// Error.
        /// 错误。
        /// </summary>
        Error = 3,

        /// <summary>
        /// Fatal.
        /// 严重。
        /// </summary>
        Fatal = 4
    }
}