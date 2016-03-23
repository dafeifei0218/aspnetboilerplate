using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Threading;

namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for
    /// <see cref="INotificationSubscriptionManager"/>, 
    /// <see cref="INotificationPublisher"/>, 
    /// <see cref="IUserNotificationManager"/>.
    /// 通知扩展方法
    /// </summary>
    public static class NotificationExtensions
    {
        #region INotificationSubscriptionManager

        /// <summary>
        /// Subscribes to a notification.
        /// 订阅一个通知。
        /// </summary>
        /// <param name="notificationSubscriptionManager">Notification subscription manager 通知订阅管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="tenantId">租户Id</param>
        /// <param name="entityIdentifier">entity identifier 实体标识</param>
        public static void Subscribe(this INotificationSubscriptionManager notificationSubscriptionManager, int? tenantId, long userId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            AsyncHelper.RunSync(() => notificationSubscriptionManager.SubscribeAsync(tenantId, userId, notificationName, entityIdentifier));
        }

        /// <summary>
        /// Subscribes to all available notifications for given user.
        /// It does not subscribe entity related notifications.
        /// 订阅给定用户所有可用的通知。
        /// 它不订阅实体相关的通知。
        /// </summary>
        /// <param name="notificationSubscriptionManager">Notification subscription manager 通知订阅管理类</param>
        /// <param name="tenantId">The tenant identifier. 租户标识</param>
        /// <param name="userId">The user identifier. 用户Id</param>
        public static void SubscribeToAllAvailableNotifications(this INotificationSubscriptionManager notificationSubscriptionManager, int? tenantId, long userId)
        {
            AsyncHelper.RunSync(() => notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(tenantId, userId));            
        }

        /// <summary>
        /// Unsubscribes from a notification.
        /// 取消订阅一个通知。
        /// </summary>
        /// <param name="notificationSubscriptionManager">Notification subscription manager 通知订阅管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识</param>
        public static void Unsubscribe(this INotificationSubscriptionManager notificationSubscriptionManager, long userId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            AsyncHelper.RunSync(() => notificationSubscriptionManager.UnsubscribeAsync(userId, notificationName, entityIdentifier));
        }

        /// <summary>
        /// Gets all subscribtions for given notification.
        /// 获取给定通知的全部订阅。
        /// </summary>
        /// <param name="notificationSubscriptionManager">Notification subscription manager 通知订阅管理类</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识</param>
        public static List<NotificationSubscription> GetSubscriptions(this INotificationSubscriptionManager notificationSubscriptionManager, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            return AsyncHelper.RunSync(() => notificationSubscriptionManager.GetSubscriptionsAsync(notificationName, entityIdentifier));
        }

        /// <summary>
        /// Gets all subscribtions for given notification.
        /// 获取给定通知的全部订阅。
        /// </summary>
        /// <param name="notificationSubscriptionManager">Notification subscription manager 通知订阅管理类</param>
        /// <param name="tenantId">Tenant id. Null for the host. 租户Id。host为空</param>
        /// <param name="notificationName">Name of the notification. 通知名称</param>
        /// <param name="entityIdentifier">entity identifier 实体标识</param>
        public static List<NotificationSubscription> GetSubscriptions(this INotificationSubscriptionManager notificationSubscriptionManager, int? tenantId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            return AsyncHelper.RunSync(() => notificationSubscriptionManager.GetSubscriptionsAsync(tenantId, notificationName, entityIdentifier));
        }

        /// <summary>
        /// Gets subscribed notifications for a user.
        /// 获取给定用户的订阅通知。
        /// </summary>
        /// <param name="notificationSubscriptionManager">Notification subscription manager 通知订阅管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        public static List<NotificationSubscription> GetSubscribedNotifications(this INotificationSubscriptionManager notificationSubscriptionManager, long userId)
        {
            return AsyncHelper.RunSync(() => notificationSubscriptionManager.GetSubscribedNotificationsAsync(userId));
        }

        /// <summary>
        /// Checks if a user subscribed for a notification.
        /// 检查用户是否订阅通知。
        /// </summary>
        /// <param name="notificationSubscriptionManager">Notification subscription manager 通知订阅管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier 实体标识</param>
        public static bool IsSubscribed(this INotificationSubscriptionManager notificationSubscriptionManager, long userId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            return AsyncHelper.RunSync(() => notificationSubscriptionManager.IsSubscribedAsync(userId, notificationName, entityIdentifier));
        }

        #endregion

        #region INotificationPublisher

        /// <summary>
        /// Publishes a new notification.
        /// 发布一个通知
        /// </summary>
        /// <param name="notificationPublisher">Notification publisher 通知发布</param>
        /// <param name="notificationName">Unique notification name 通知名称</param>
        /// <param name="data">Notification data (optional) 通知数据（可选）</param>
        /// <param name="entityIdentifier">The entity identifier if this notification is related to an entity 实体标识</param>
        /// <param name="severity">Notification severity 通知严重程度</param>
        /// <param name="userIds">Target user id(s). Used to send notification to specific user(s). If this is null/empty, the notification is sent to all subscribed users 用户Id</param>
        public static void Publish(this INotificationPublisher notificationPublisher, string notificationName, NotificationData data = null, EntityIdentifier entityIdentifier = null, NotificationSeverity severity = NotificationSeverity.Info, long[] userIds = null)
        {
            AsyncHelper.RunSync(() => notificationPublisher.PublishAsync(notificationName, data, entityIdentifier, severity, userIds));
        }

        #endregion

        #region IUserNotificationManager

        /// <summary>
        /// Gets notifications for a user.
        /// 获取给定用户的通知
        /// </summary>
        /// <param name="userNotificationManager">User notificaiton manager 用户通知管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="state">State 状态</param>
        /// <param name="skipCount">Skip count. 跳过条数</param>
        /// <param name="maxResultCount">Maximum result count. 最大结果数</param>
        public static List<UserNotification> GetUserNotifications(this IUserNotificationManager userNotificationManager, long userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            return AsyncHelper.RunSync(() => userNotificationManager.GetUserNotificationsAsync(userId, state, skipCount: skipCount, maxResultCount: maxResultCount));
        }

        /// <summary>
        /// Gets user notification count.
        /// 获取用户通知书
        /// </summary>
        /// <param name="userNotificationManager">User notificaiton manager 用户通知管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="state">State. 状态</param>
        public static int GetUserNotificationCount(this IUserNotificationManager userNotificationManager, long userId, UserNotificationState? state = null)
        {
            return AsyncHelper.RunSync(() => userNotificationManager.GetUserNotificationCountAsync(userId, state));
        }

        /// <summary>
        /// Gets a user notification by given id.
        /// 获取给定id的用户通知。
        /// </summary>
        /// <param name="userNotificationManager">User notificaiton manager 用户通知管理类</param>
        /// <param name="userNotificationId">The user notification id. 用户通知Id</param>
        public static UserNotification GetUserNotification(this IUserNotificationManager userNotificationManager, Guid userNotificationId)
        {
            return AsyncHelper.RunSync(() => userNotificationManager.GetUserNotificationAsync(userNotificationId));
        }

        /// <summary>
        /// Updates a user notification state.
        /// 更新用户通知状态。
        /// </summary>
        /// <param name="userNotificationManager">User notificaiton manager 用户通知管理类</param>
        /// <param name="userNotificationId">The user notification id. 用户通知Id</param>
        /// <param name="state">New state. 新状态</param>
        public static void UpdateUserNotificationState(this IUserNotificationManager userNotificationManager, Guid userNotificationId, UserNotificationState state)
        {
            AsyncHelper.RunSync(() => userNotificationManager.UpdateUserNotificationStateAsync(userNotificationId, state));
        }

        /// <summary>
        /// Updates all notification states for a user.
        /// 更新用户的全部通知状态。
        /// </summary>
        /// <param name="userNotificationManager">User notificaiton manager 用户通知管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="state">New state. 新状态</param>
        public static void UpdateAllUserNotificationStates(this IUserNotificationManager userNotificationManager, long userId, UserNotificationState state)
        {
            AsyncHelper.RunSync(() => userNotificationManager.UpdateAllUserNotificationStatesAsync(userId, state));
        }

        /// <summary>
        /// Deletes a user notification.
        /// 删除一个用户通知。
        /// </summary>
        /// <param name="userNotificationManager">User notificaiton manager 用户通知管理类</param>
        /// <param name="userNotificationId">The user notification id. 用户通知Id</param>
        public static void DeleteUserNotification(this IUserNotificationManager userNotificationManager, Guid userNotificationId)
        {
            AsyncHelper.RunSync(() => userNotificationManager.DeleteUserNotificationAsync(userNotificationId));
        }

        /// <summary>
        /// Deletes all notifications of a user.
        /// 删除用户的全部通知。
        /// </summary>
        /// <param name="userNotificationManager">User notificaiton manager 用户通知管理类</param>
        /// <param name="userId">The user id. 用户Id</param>
        public static void DeleteAllUserNotifications(this IUserNotificationManager userNotificationManager, long userId)
        {
            AsyncHelper.RunSync(() => userNotificationManager.DeleteAllUserNotificationsAsync(userId));
        }

        #endregion
    }
}