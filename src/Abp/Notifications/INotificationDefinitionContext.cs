namespace Abp.Notifications
{
    /// <summary>
    /// Used as a context while defining notifications.
    /// 通知定义上下文接口，定义通知时使用的上下文。
    /// </summary>
    public interface INotificationDefinitionContext
    {
        /// <summary>
        /// Gets the notification definition manager.
        /// 获取通知定义管理类
        /// </summary>
        INotificationDefinitionManager Manager { get; }
    }
}