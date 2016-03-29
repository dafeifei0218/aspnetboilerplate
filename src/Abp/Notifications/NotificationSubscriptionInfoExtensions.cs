using System;
using Abp.Domain.Entities;
using Abp.Extensions;
using Newtonsoft.Json;

namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="NotificationSubscriptionInfo"/>.
    /// 通知订阅的信息扩展类
    /// </summary>
    public static class NotificationSubscriptionInfoExtensions
    {
        /// <summary>
        /// Converts <see cref="UserNotificationInfo"/> to <see cref="UserNotification"/>.
        /// 转换<see cref="UserNotificationInfo"/>用户通知信息到<see cref="UserNotification"/>用户通知。
        /// </summary>
        public static NotificationSubscription ToNotificationSubscription(this NotificationSubscriptionInfo subscriptionInfo)
        {
            var entityType = subscriptionInfo.EntityTypeAssemblyQualifiedName.IsNullOrEmpty()
                ? null
                : Type.GetType(subscriptionInfo.EntityTypeAssemblyQualifiedName);

            return new NotificationSubscription
            {
                UserId = subscriptionInfo.UserId,
                NotificationName = subscriptionInfo.NotificationName,
                EntityType = entityType,
                EntityTypeName = subscriptionInfo.EntityTypeName,
                EntityId = subscriptionInfo.EntityId.IsNullOrEmpty() ? null : JsonConvert.DeserializeObject(subscriptionInfo.EntityId, EntityHelper.GetPrimaryKeyType(entityType))
            };
        }
    }
}