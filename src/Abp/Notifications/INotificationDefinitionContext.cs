namespace Abp.Notifications
{
    /// <summary>
    /// Used as a context while defining notifications.
    /// 通知定义上下文接口，定义通知时使用的上下文。
    /// </summary>
    /// <remarks>
    /// 上下文类，作为方法的参数。
    /// 没有特别的业务逻辑。
    /// 这边context只是封装了INotificationDefinitionManager对象。
    /// </remarks>
    public interface INotificationDefinitionContext
    {
        /// <summary>
        /// Gets the notification definition manager.
        /// 获取通知定义管理类
        /// </summary>
        INotificationDefinitionManager Manager { get; }
    }
}