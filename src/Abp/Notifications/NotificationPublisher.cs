using System;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Json;
using Abp.Runtime.Session;

namespace Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationPublisher"/>.
    /// 通知发布
    /// </summary>
    public class NotificationPublisher : AbpServiceBase, INotificationPublisher, ITransientDependency
    {
        /// <summary>
        /// Indicates all tenants.
        /// 全部租户
        /// </summary>
        public int[] AllTenants
        {
            get
            {
                return new[] { NotificationInfo.AllTenantIds.To<int>() };
            }
        }

        /// <summary>
        /// Reference to ABP session.
        /// ABP会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        private readonly INotificationStore _store;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly INotificationDistributer _notificationDistributer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPublisher"/> class.
        /// 初始化一个新的<see cref="NotificationPublisher"/>实例
        /// </summary>
        public NotificationPublisher(
            INotificationStore store,
            IBackgroundJobManager backgroundJobManager,
            INotificationDistributer notificationDistributer)
        {
            _store = store;
            _backgroundJobManager = backgroundJobManager;
            _notificationDistributer = notificationDistributer;
            AbpSession = NullAbpSession.Instance;
        }

        //Create EntityIdentifier includes entityType and entityId.
        /// <summary>
        /// 发布-异步 
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        /// <param name="data">通知数据</param>
        /// <param name="entityIdentifier">实体标识</param>
        /// <param name="severity">通知严重程度</param>
        /// <param name="userIds">用户Id</param>
        /// <param name="excludedUserIds">排除用户Id</param>
        /// <param name="tenantIds">租户Id</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task PublishAsync(
            string notificationName,
            NotificationData data = null,
            EntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            long[] userIds = null,
            long[] excludedUserIds = null,
            int?[] tenantIds = null)
        {
            if (notificationName.IsNullOrEmpty())
            {
                throw new ArgumentException("NotificationName can not be null or whitespace!", "notificationName");
            }

            if (!tenantIds.IsNullOrEmpty() && !userIds.IsNullOrEmpty())
            {
                throw new ArgumentException("tenantIds can be set only userIds is not set!", "tenantIds");
            }

            if (tenantIds.IsNullOrEmpty() && userIds.IsNullOrEmpty())
            {
                tenantIds = new[] {AbpSession.TenantId};
            }

            var notificationInfo = new NotificationInfo
            {
                NotificationName = notificationName,
                EntityTypeName = entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                EntityTypeAssemblyQualifiedName = entityIdentifier == null ? null : entityIdentifier.Type.AssemblyQualifiedName,
                EntityId = entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString(),
                Severity = severity,
                UserIds = userIds.IsNullOrEmpty() ? null : userIds.JoinAsString(","),
                ExcludedUserIds = excludedUserIds.IsNullOrEmpty() ? null : excludedUserIds.JoinAsString(","),
                TenantIds = tenantIds.IsNullOrEmpty() ? null : tenantIds.JoinAsString(","),
                Data = data == null ? null : data.ToJsonString(),
                DataTypeName = data == null ? null : data.GetType().AssemblyQualifiedName
            };

            await _store.InsertNotificationAsync(notificationInfo);

            await CurrentUnitOfWork.SaveChangesAsync(); //To get Id of the notification 获得通知的身份证

            if (userIds != null && userIds.Length <= 5)
            {
                //We can directly distribute the notification since there are not much receivers
                //我们可以直接分发通知，因为没有太多的接收器
                await _notificationDistributer.DistributeAsync(notificationInfo.Id);
            }
            else
            {
                //We enqueue a background job since distributing may get a long time
                //我们将后台工作分配可能会很长一段时间以来
                await _backgroundJobManager.EnqueueAsync<NotificationDistributionJob, NotificationDistributionJobArgs>(
                    new NotificationDistributionJobArgs(
                        notificationInfo.Id
                        )
                    );
            }
        }
    }
}