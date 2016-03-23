using System.Collections.Generic;
using Abp.Threading;

namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="INotificationDefinitionManager"/>.
    /// ֪ͨ���������չ��
    /// </summary>
    public static class NotificationDefinitionManagerExtensions
    {
        /// <summary>
        /// Gets all available notification definitions for given <see cref="tenantId"/> and <see cref="userId"/>.
        /// ��ȡȫ������֪ͨ����
        /// </summary>
        /// <param name="notificationDefinitionManager">Notification definition manager ֪ͨ���������</param>
        /// <param name="tenantId">Tenant id �⻧Id</param>
        /// <param name="userId">User id �û�Id</param>
        public static IReadOnlyList<NotificationDefinition> GetAllAvailable(this INotificationDefinitionManager notificationDefinitionManager, int? tenantId, long userId)
        {
            return AsyncHelper.RunSync(() => notificationDefinitionManager.GetAllAvailableAsync(tenantId, userId));
        }
    }
}