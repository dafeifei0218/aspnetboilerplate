using Abp.Dependency;

namespace Abp.Notifications
{
    /// <summary>
    /// This class should be implemented in order to define notifications.
    /// 通知提供者，
    /// 定义通知，应该被实现。
    /// </summary>
    /// <remarks>
    /// 抽象基类，用于向INotificationDefinitionManager对象（NotificationDefinitionManager）中添加NotificationDefinition。
    /// Abp框架只提供了抽象类，实际项目中可以创建自定义NotificationProvider来从数据库中读取NotificationDefinition来填充到NotificationDefinitionManager对象中。
    /// </remarks>
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