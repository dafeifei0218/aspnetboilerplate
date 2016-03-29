using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Null pattern implementation of <see cref="INotificationStore"/>.
    /// 空通知存储
    /// </summary>
    public class NullNotificationStore : INotificationStore
    {
        /// <summary>
        /// 插入订阅-异步
        /// </summary>
        /// <param name="subscription">通知订阅的信息</param>
        /// <returns></returns>
        public Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 删除订阅-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="entityId">实体Id</param>
        /// <returns></returns>
        public Task DeleteSubscriptionAsync(long userId, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 插入通知-异步
        /// </summary>
        /// <param name="notification">通知信息</param>
        /// <returns></returns>
        public Task InsertNotificationAsync(NotificationInfo notification)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取通知-异步
        /// </summary>
        /// <param name="notificationId">通知Id</param>
        /// <returns></returns>
        public Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId)
        {
            return Task.FromResult(null as NotificationInfo);
        }

        /// <summary>
        /// 插入用户通知-异步
        /// </summary>
        /// <param name="userNotification">用户通知</param>
        /// <returns></returns>
        public Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取订阅-异步
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">通知类型名称</param>
        /// <param name="entityId">实体Id</param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName = null, string entityId = null)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// 获取订阅-异步
        /// </summary>
        /// <param name="tenantIds">租户Id</param>
        /// <param name="notificationName"></param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="entityId">实体Id</param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int?[] tenantIds, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// 获取订阅-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(long userId)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// 是否订阅-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="notificationName">通知名称</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="entityId">实体Id</param>
        /// <returns></returns>
        public Task<bool> IsSubscribedAsync(long userId, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// 更新用户通知状态-异步
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        /// <param name="state">用户通知状态</param>
        /// <returns></returns>
        public Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 更新全部用户通知状态-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="state">用户通知状态</param>
        /// <returns></returns>
        public Task UpdateAllUserNotificationStatesAsync(long userId, UserNotificationState state)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 删除用户通知-异步
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        /// <returns></returns>
        public Task DeleteUserNotificationAsync(Guid userNotificationId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 删除全部用户通知-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Task DeleteAllUserNotificationsAsync(long userId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户通知-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="state">状态</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <returns></returns>
        public Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(long userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            return Task.FromResult(new List<UserNotificationInfoWithNotificationInfo>());
        }

        /// <summary>
        /// 获取用户通知数-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public Task<int> GetUserNotificationCountAsync(long userId, UserNotificationState? state = null)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取用户通知和通知为空-异步
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        /// <returns></returns>
        public Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(Guid userNotificationId)
        {
            return Task.FromResult((UserNotificationInfoWithNotificationInfo)null);
        }
    }
}