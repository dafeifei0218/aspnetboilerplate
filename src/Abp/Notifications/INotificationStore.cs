using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to store (persist) notifications.
    /// 通知存储接口，用户存储（持久）通知。
    /// </summary>
    /// <remarks>
    /// 该接口提供持久化NotificationInfo的方法。
    /// NullNotificationStore是其空的实现。
    /// 具体的实现留到外部的模块中。
    /// </remarks>
    public interface INotificationStore
    {
        /// <summary>
        /// Inserts a notification subscription.
        /// 插入一个通知订阅-异步。
        /// </summary>
        /// <param name="subscription">通知订阅的信息</param>
        Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription);

        /// <summary>
        /// Deletes a notification subscription.
        /// 删除一个通知订阅-异步。
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="entityId">实体Id</param>
        Task DeleteSubscriptionAsync(long userId, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Inserts a notification.
        /// 插入一个通知订阅-异步。
        /// </summary> 
        /// <param name="notification">通知信息</param>
        Task InsertNotificationAsync(NotificationInfo notification);

        /// <summary>
        /// Gets a notification by Id, or returns null if not found.
        /// 获取给定通知Id的通知，如果未找到返回null，异步。
        /// </summary>
        /// <param name="notificationId">通知Id</param>
        Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId);

        /// <summary>
        /// Inserts a user notification.
        /// 插入一个通知订阅-异步。
        /// </summary>
        /// <param name="userNotification">用户通知信息</param>
        Task InsertUserNotificationAsync(UserNotificationInfo userNotification);

        /// <summary>
        /// Gets subscriptions for a notification.
        /// 获取通知的订阅-异步。
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="entityId">实体Id</param>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Gets subscriptions for a notification for specified tenant(s).
        /// 获取指定租户Id的通知的订阅
        /// </summary>
        /// <param name="tenantIds">租户Id</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="entityId">实体Id</param>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int?[] tenantIds, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Gets subscriptions for a user.
        /// 获取给定用户的订阅
        /// </summary>
        /// <param name="userId">用户Id</param> 
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(long userId);

        /// <summary>
        /// Checks if a user subscribed for a notification
        /// 检查用户是否定义通知。
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="entityId">实体Id</param>
        Task<bool> IsSubscribedAsync(long userId, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Updates a user notification state.
        /// 更新用户通知状态。
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        /// <param name="state">状态</param>
        Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// 更新全部用户通知状态。
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="state">状态</param>
        Task UpdateAllUserNotificationStatesAsync(long userId, UserNotificationState state);

        /// <summary>
        /// Deletes a user notification.
        /// 删除用户通知。
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        Task DeleteUserNotificationAsync(Guid userNotificationId);

        /// <summary>
        /// Deletes all notifications of a user.
        /// 删除用户Id的全部通知。
        /// </summary>
        /// <param name="userId">用户Id</param>
        Task DeleteAllUserNotificationsAsync(long userId);

        /// <summary>
        /// Gets notifications of a user.
        /// 获取用户通知
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="skipCount">Skip count. 跳过数</param>
        /// <param name="maxResultCount">Maximum result count. 最大结果数</param>
        /// <param name="state">State 状态</param>
        Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(long userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue);

        /// <summary>
        /// Gets user notification count.
        /// 获取用户通知数量
        /// </summary>
        /// <param name="userId">The user identifier. 用户Id</param>
        /// <param name="state">The state. 状态</param>
        Task<int> GetUserNotificationCountAsync(long userId, UserNotificationState? state = null);

        /// <summary>
        /// Gets a user notification.
        /// 获取用户通知
        /// </summary>
        /// <param name="userNotificationId">Skip count. 用户通知Id</param>
        Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(Guid userNotificationId);
    }
}