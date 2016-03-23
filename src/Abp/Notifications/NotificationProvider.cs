using Abp.Dependency;

namespace Abp.Notifications
{
    /// <summary>
    /// This class should be implemented in order to define notifications.
    /// 通知提供者，
    /// 定义通知，应该被实现。
    /// </summary>
    public abstract class NotificationProvider : ITransientDependency
    {
        /// <summary>
        /// Used to add/manipulate notification definitions.
        /// 设置通知，用于添加/操作通知定义。
        /// </summary>
        /// <param name="context">Context 通知定义上下文</param>
        public abstract void SetNotifications(INotificationDefinitionContext context);
    }
}