using System;

namespace Abp.Notifications
{
    /// <summary>
    /// Arguments for <see cref="NotificationDistributionJob"/>.
    /// 通知分发作业参数
    /// </summary>
    [Serializable]
    public class NotificationDistributionJobArgs
    {
        /// <summary>
        /// Notification Id.
        /// 通知Id
        /// </summary>
        public Guid NotificationId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJobArgs"/> class.
        /// 实例化一个<see cref="NotificationDistributionJobArgs"/>通知分发作业参数类
        /// </summary>
        public NotificationDistributionJobArgs(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}