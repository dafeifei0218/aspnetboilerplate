using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Abp.Notifications
{
    /// <summary>
    /// Implements  <see cref="IUserNotificationManager"/>.
    /// 用户通知管理，实现<see cref="IUserNotificationManager"/>用户通知管理接口
    /// </summary>
    public class UserNotificationManager : IUserNotificationManager, ISingletonDependency
    {
        /// <summary>
        /// 通知存储
        /// </summary>
        private readonly INotificationStore _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationManager"/> class.
        /// 初始化一个新实例<see cref="UserNotificationManager"/>用户通知管理类。
        /// </summary>
        public UserNotificationManager(INotificationStore store)
        {
            _store = store;
        }

        /// <summary>
        /// 获取用户通知-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="state">用户通知状态</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <returns></returns>
        public async Task<List<UserNotification>> GetUserNotificationsAsync(long userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            var userNotifications = await _store.GetUserNotificationsWithNotificationsAsync(userId, state, skipCount, maxResultCount);
            return userNotifications
                .Select(un => un.ToUserNotification())
                .ToList();
        }

        /// <summary>
        /// 获取用户通知数-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="state">用户通知状态</param>
        /// <returns></returns>
        public Task<int> GetUserNotificationCountAsync(long userId, UserNotificationState? state = null)
        {
            return _store.GetUserNotificationCountAsync(userId, state);
        }

        /// <summary>
        /// 获取用户通知-异步
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        /// <returns></returns>
        public async Task<UserNotification> GetUserNotificationAsync(Guid userNotificationId)
        {
            var userNotification = await _store.GetUserNotificationWithNotificationOrNullAsync(userNotificationId);
            if (userNotification == null)
            {
                return null;
            }

            return userNotification.ToUserNotification();
        }

        /// <summary>
        /// 更新用户通知状态-异步
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        /// <param name="state">用户通知状态</param>
        /// <returns></returns>
        public Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state)
        {
            return _store.UpdateUserNotificationStateAsync(userNotificationId, state);
        }

        /// <summary>
        /// 更新全部用户通知状态-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="state">用户通知状态</param>
        /// <returns></returns>
        public Task UpdateAllUserNotificationStatesAsync(long userId, UserNotificationState state)
        {
            return _store.UpdateAllUserNotificationStatesAsync(userId, state);
        }

        /// <summary>
        /// 删除用户通知状态-异步
        /// </summary>
        /// <param name="userNotificationId">用户通知Id</param>
        /// <returns></returns>
        public Task DeleteUserNotificationAsync(Guid userNotificationId)
        {
            return _store.DeleteUserNotificationAsync(userNotificationId);
        }

        /// <summary>
        /// 删除全部用户通知状态-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Task DeleteAllUserNotificationsAsync(long userId)
        {
            return _store.DeleteAllUserNotificationsAsync(userId);
        }
    }
}