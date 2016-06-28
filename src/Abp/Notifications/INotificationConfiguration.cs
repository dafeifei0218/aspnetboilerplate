using Abp.Collections;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to configure notification system.
    /// 通知配置接口，用于配置通知系统。
    /// </summary>
    /// <remarks>
    /// 提供给外部配置NotificationProvider。
    /// NotificationDefinitionManager通过具体的NotificationProvider来初始化和装载Notification字典对象。
    /// 但是ABP核心模块处于最底层，怎么能知道上层定义的NotificationProvider的类型呢？ 
    /// NotificationConfiguration为解决这个问题引入了NotificationProvider配置项。
    /// NotificationProvider就是一个Type 列表 (ITypeList<NotificationProvider>),注意是NotificationProvider的Type，不是实例。
    /// 在需要NotificationProvider的地方，可以使用容器根据Type构造出实例。
    /// </remarks>
    public interface INotificationConfiguration
    {
        /// <summary>
        /// Notification providers.
        /// 通知提供者
        /// </summary>
        ITypeList<NotificationProvider> Providers { get; }
    }
}