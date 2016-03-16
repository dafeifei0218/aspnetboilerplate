using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// 通知分发接口，用于向用户分发通知。
    /// </summary>
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