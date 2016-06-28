using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Threading;

namespace Abp.Notifications
{
    /// <summary>
    /// This background job distributes notifications to users.
    /// 通知分发作业，
    /// 后台作业分发通知给用户。
    /// </summary>
    /// <remarks>
    /// 封装了INotificationDistributer的后台任务，当Notification的接收者超过5人时会，ABP将分发任务封装为一个后台执行任务，以减少用户等待时间。
    /// 5是被hardcode（硬编码）到源码中的（NotificationPublisher类的PublishAsync方法）。
    /// </remarks>
    public class NotificationDistributionJob : BackgroundJob<NotificationDistributionJobArgs>, ITransientDependency
    {
        private readonly INotificationDistributer _notificationDistributer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJob"/> class.
        /// 实例化一个新<see cref="NotificationDistributionJob"/>通知分发作业类
        /// </summary>
        public NotificationDistributionJob(INotificationDistributer notificationDistributer)
        {
            _notificationDistributer = notificationDistributer;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args">通知分发参数</param>
        public override void Execute(NotificationDistributionJobArgs args)
        {
            AsyncHelper.RunSync(() => _notificationDistributer.DistributeAsync(args.NotificationId));
        }
    }
}
