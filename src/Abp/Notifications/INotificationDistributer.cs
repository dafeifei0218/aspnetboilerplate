using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// 通知分发接口，用于向用户分发通知。
    /// </summary>
    /// <remarks>
    /// 用于分发Notification给User，也就是建立Notification和User的关系。
    /// 在分发Notification给某个User前会检查User的NotificationSetting，如果该设置为True就建立Notification和该User的关系
    /// </remarks>
    public interface INotificationDistributer : IDomainService
    {
        /// <summary>
        /// Distributes given notification to users.
        /// 分发给用户的通知。
        /// </summary>
        /// <param name="notificationId">The notification id. 通知Id</param>
        Task DistributeAsync(Guid notificationId);
    }
}