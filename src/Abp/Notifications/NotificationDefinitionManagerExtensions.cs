using System.Collections.Generic;
using Abp.Threading;

namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="INotificationDefinitionManager"/>.
    /// 通知定义管理扩展类
    /// </summary>
    public static class NotificationDefinitionManagerExtensions
    {
        /// <summary>
        /// Gets all available notification definitions for given <see cref="tenantId"/> and <see cref="userId"/>.
        /// 获取全部可用通知定义
        /// </summary>
        /// <param name="notificationDefinitionManager">Notification definition manager 通知定义管理类</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <param name="userId">User id 用户Id</param>
        public static IReadOnlyList<NotificationDefinition> GetAllAvailable(this INotificationDefinitionManager notificationDefinitionManager, int? tenantId, long userId)
        {
            return AsyncHelper.RunSync(() => notificationDefinitionManager.GetAllAvailableAsync(tenantId, userId));
        }
    }
}