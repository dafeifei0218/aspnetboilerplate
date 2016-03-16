using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Runtime.Session;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to publish notifications.
    /// 通知发布接口，用于发布通知。
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// Publishes a new notification.
        /// 发布一个新的通知。
        /// </summary>
        /// <param name="notificationName">Unique notification name 通知名称</param>
        /// <param name="data">Notification data (optional) 通知数据（可选）</param>
        /// <param name="entityIdentifier">The entity identifier if this notification is related to an entity 如果该通知与实体有关，则该实体标识符</param>
        /// <param name="severity">Notification severity 通知安全</param>
        /// <param name="userIds">
        /// Target user id(s). 
        /// Used to send notification to specific user(s). 
        /// If this is null/empty, the notification is sent to subscribed users.
        /// 目标用户Id。
        /// </param>
        /// <param name="excludedUserIds">
        /// Excluded user id(s).
        /// This can be set to exclude some users while publishing notifications to subscribed users.
        /// It's normally not set if <see cref="userIds"/> is set.
        /// 排除用户Id
        /// </param>
        /// <param name="tenantIds">
        /// Target tenant id(s).
        /// Used to send notification to subscribed users of specific tenant(s).
        /// This should not be set if <see cref="userIds"/> is set.
        /// <see cref="NotificationPublisher.AllTenants"/> can be passed to indicate all tenants.
        /// If this is null, then it's automatically set to the current tenant on <see cref="IAbpSession.TenantId"/>.
        /// 目标租户Id
        /// </param>
        Task PublishAsync(
            string notificationName,
            NotificationData data = null,
            EntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            long[] userIds = null,
            long[] excludedUserIds = null,
            int?[] tenantIds = null);
    }
}