using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to manage subscriptions for notifications.
    /// 通知订阅管理接口，用于管理通知订阅。
    /// </summary>
    public interface INotificationSubscriptionManager
    {
        /// <summary>
        /// Subscribes to a notification for given user and notification informations.
        /// 订阅-异步，
        /// 订阅给定用户和通知信息的通知
        /// </summary>
        /// <param name="tenantId">Tenant id of the user. Null for host users. 租户Id</param>
        /// <param name="userId">The user id (which belongs to given tenantId). 用户Id</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识符</param>
        Task SubscribeAsync(int? tenantId, long userId, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Subscribes to all available notifications for given user.
        /// It does not subscribe entity related notifications.
        /// 订阅用户所有可用的通知。
        /// 他不订阅实体相关的通知。
        /// </summary>
        /// <param name="tenantId">The tenant identifier. 租户Id</param>
        /// <param name="userId">The user identifier. 用户Id</param>
        Task SubscribeToAllAvailableNotificationsAsync(int? tenantId, long userId);

        /// <summary>
        /// Unsubscribes from a notification.
        /// 退订一个通知。
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识符</param>
        Task UnsubscribeAsync(long userId, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>subscriptions
        /// Gets all subscribtions for given notification (including all tenants).
        /// 获取给定通知所有订阅（包括租户）。
        /// </summary>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识符</param>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets all subscribtions for given notification.
        /// 获取给定通知所有订阅。
        /// </summary>
        /// <param name="tenantId">Tenant id. Null for the host. 租户Id</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识符</param>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(int? tenantId, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets subscribed notifications for a user.
        /// 获取给定用户的订阅通知。
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        Task<List<NotificationSubscription>> GetSubscribedNotificationsAsync(long userId);

        /// <summary>
        /// Checks if a user subscribed for a notification.
        /// 检查用户是否订阅通知。
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识符</param>
        Task<bool> IsSubscribedAsync(long userId, string notificationName, EntityIdentifier entityIdentifier = null);
    }
}
