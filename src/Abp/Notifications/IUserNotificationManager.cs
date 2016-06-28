using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to manage user notifications.
    /// 用户通知管理接口，用于管理用户通知。
    /// </summary>
    /// <remarks>
    /// 用于获取，删除UserNotification，以及更改UserNotification的状态。
    /// </remarks>
    public interface IUserNotificationManager
    {
        /// <summary>
        /// Gets notifications for a user.
        /// 获取给定用户的通知
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="state">State 用户通知状态</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        Task<List<UserNotification>> GetUserNotificationsAsync(long userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue);

        /// <summary>
        /// Gets user notification count.
        /// 获取用户通知数量
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="state">State. 用户通知状态</param>
        Task<int> GetUserNotificationCountAsync(long userId, UserNotificationState? state = null);

        /// <summary>
        /// Gets a user notification by given id.
        /// 获取给定用户通知Id的用户通知。
        /// </summary>
        /// <param name="userNotificationId">The user notification id. 用户通知Id</param>
        Task<UserNotification> GetUserNotificationAsync(Guid userNotificationId);

        /// <summary>
        /// Updates a user notification state.
        /// 更新用户通知的状态
        /// </summary>
        /// <param name="userNotificationId">The user notification id. 用户通知Id</param>
        /// <param name="state">New state. 用户通知状态</param>
        Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// 更新用户的全部通知状态。
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        /// <param name="state">New state. 用户通知状态</param>
        Task UpdateAllUserNotificationStatesAsync(long userId, UserNotificationState state);

        /// <summary>
        /// Deletes a user notification.
        /// 删除用户通知状态。
        /// </summary>
        /// <param name="userNotificationId">The user notification id. 用户通知Id</param>
        Task DeleteUserNotificationAsync(Guid userNotificationId);

        /// <summary>
        /// Deletes all notifications of a user.
        /// 删除全部用户通知状态
        /// </summary>
        /// <param name="userId">The user id. 用户Id</param>
        Task DeleteAllUserNotificationsAsync(long userId);
    }
}