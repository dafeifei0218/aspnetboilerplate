using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Extensions;
using Castle.Core.Internal;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// 通知分发，用于分发通知给用户
    /// </summary>
    public class NotificationDistributer : DomainService, INotificationDistributer
    {
        /// <summary>
        /// Referece to <see cref="IRealTimeNotifier"/>.
        /// 实时通知
        /// </summary>
        public IRealTimeNotifier RealTimeNotifier { get; set; }

        private readonly INotificationDefinitionManager _notificationDefinitionManager;
        private readonly INotificationStore _notificationStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJob"/> class.
        /// 初始化一个新实例<see cref="NotificationDistributer"/>类
        /// </summary>
        public NotificationDistributer(
            INotificationDefinitionManager notificationDefinitionManager,
            INotificationStore notificationStore)
        {
            _notificationDefinitionManager = notificationDefinitionManager;
            _notificationStore = notificationStore;

            RealTimeNotifier = NullRealTimeNotifier.Instance;
        }

        /// <summary>
        /// 分发-异步
        /// </summary>
        /// <param name="notificationId">分发Id</param>
        /// <returns></returns>
        public async Task DistributeAsync(Guid notificationId)
        {
            var notificationInfo = await _notificationStore.GetNotificationOrNullAsync(notificationId);
            if (notificationInfo == null)
            {
                Logger.Warn("NotificationDistributionJob can not continue since could not found notification by id: " + notificationId);
                return;
            }

            Notification notification;

            try
            {
                notification = notificationInfo.ToNotification();
            }
            catch (Exception)
            {
                Logger.Warn("NotificationDistributionJob can not continue since could not convert NotificationInfo to Notification for NotificationId: " + notificationId);
                return;
            }

            var userIds = await GetUserIds(notificationInfo);

            var userNotificationInfos = userIds
                .Select(userId => new UserNotificationInfo(userId, notificationInfo.Id))
                .ToList();

            await SaveUserNotifications(userNotificationInfos);

            try
            {
                var userNotifications = userNotificationInfos
                    .Select(uni => uni.ToUserNotification(notification))
                    .ToArray();

                await RealTimeNotifier.SendNotificationsAsync(userNotifications);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <param name="notificationInfo">通知信息</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual async Task<long[]> GetUserIds(NotificationInfo notificationInfo)
        {
            List<long> userIds;

            if (!notificationInfo.UserIds.IsNullOrEmpty())
            {
                //Directly get from UserIds
                userIds = notificationInfo
                    .UserIds
                    .Split(",")
                    .Select(uidAsStr => uidAsStr.To<long>())
                    .Where(uid => SettingManager.GetSettingValueForUser<bool>(NotificationSettingNames.ReceiveNotifications, null, uid))
                    .ToList();
            }
            else
            {
                //Get subscribed users
                //获取订阅用户
                var tenantIds = GetTenantIds(notificationInfo);

                List<NotificationSubscriptionInfo> subscriptions;

                if (tenantIds.IsNullOrEmpty())
                {
                    //Get all subscribed users of all tenants
                    //获取全部租户的其全部订阅用户
                    subscriptions = await _notificationStore.GetSubscriptionsAsync(
                        notificationInfo.NotificationName,
                        notificationInfo.EntityTypeName,
                        notificationInfo.EntityId
                        );
                }
                else
                {
                    //Get all subscribed users of specified tenant(s)
                    //获取指定租户的全部订阅用户
                    subscriptions = await _notificationStore.GetSubscriptionsAsync(
                        tenantIds,
                        notificationInfo.NotificationName,
                        notificationInfo.EntityTypeName,
                        notificationInfo.EntityId
                        );
                }

                //Remove invalid subscriptions
                //删除无效的订阅
                var invalidSubscriptions = new Dictionary<Guid, NotificationSubscriptionInfo>();

                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
                {

                    foreach (var subscription in subscriptions)
                    {
                        if (!await _notificationDefinitionManager.IsAvailableAsync(notificationInfo.NotificationName, subscription.TenantId, subscription.UserId) ||
                            !SettingManager.GetSettingValueForUser<bool>(NotificationSettingNames.ReceiveNotifications, subscription.TenantId, subscription.UserId))
                        {
                            invalidSubscriptions[subscription.Id] = subscription;
                        }
                    }
                }

                subscriptions.RemoveAll(s => invalidSubscriptions.ContainsKey(s.Id));

                //Get user ids
                //获取用户Id
                userIds = subscriptions
                    .Select(s => s.UserId)
                    .ToList();
            }

            if (!notificationInfo.ExcludedUserIds.IsNullOrEmpty())
            {
                //Exclude specified users.
                //排除指定的用户
                var excludedUserIds = notificationInfo
                    .ExcludedUserIds
                    .Split(",")
                    .Select(uidAsStr => uidAsStr.To<long>())
                    .ToList();

                userIds.RemoveAll(uid => excludedUserIds.Contains(uid));
            }

            return userIds.ToArray();
        }

        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <param name="notificationInfo">通知信息</param>
        /// <returns></returns>
        private static int?[] GetTenantIds(NotificationInfo notificationInfo)
        {
            if (notificationInfo.TenantIds.IsNullOrEmpty())
            {
                return null;
            }

            return notificationInfo
                .TenantIds
                .Split(",")
                .Select(uidAsStr => uidAsStr == "null" ? (int?)null : (int?)uidAsStr.To<int>())
                .ToArray();
        }

        /// <summary>
        /// 保存用户通知
        /// </summary>
        /// <param name="userNotifications">用户通知信息集合</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual async Task SaveUserNotifications(IEnumerable<UserNotificationInfo> userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {
                await _notificationStore.InsertUserNotificationAsync(userNotification);
            }

            await CurrentUnitOfWork.SaveChangesAsync(); //To get Ids of the notifications
        }
    }
}