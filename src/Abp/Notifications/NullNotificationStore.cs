using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Null pattern implementation of <see cref="INotificationStore"/>.
    /// ��֪ͨ�洢
    /// </summary>
    public class NullNotificationStore : INotificationStore
    {
        /// <summary>
        /// ���붩��-�첽
        /// </summary>
        /// <param name="subscription">֪ͨ���ĵ���Ϣ</param>
        /// <returns></returns>
        public Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ������-�첽
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityTypeName">ʵ����������</param>
        /// <param name="entityId">ʵ��Id</param>
        /// <returns></returns>
        public Task DeleteSubscriptionAsync(long userId, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ����֪ͨ-�첽
        /// </summary>
        /// <param name="notification">֪ͨ��Ϣ</param>
        /// <returns></returns>
        public Task InsertNotificationAsync(NotificationInfo notification)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ֪ͨ-�첽
        /// </summary>
        /// <param name="notificationId">֪ͨId</param>
        /// <returns></returns>
        public Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId)
        {
            return Task.FromResult(null as NotificationInfo);
        }

        /// <summary>
        /// �����û�֪ͨ-�첽
        /// </summary>
        /// <param name="userNotification">�û�֪ͨ</param>
        /// <returns></returns>
        public Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ����-�첽
        /// </summary>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityTypeName">֪ͨ��������</param>
        /// <param name="entityId">ʵ��Id</param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName = null, string entityId = null)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// ��ȡ����-�첽
        /// </summary>
        /// <param name="tenantIds">�⻧Id</param>
        /// <param name="notificationName"></param>
        /// <param name="entityTypeName">ʵ����������</param>
        /// <param name="entityId">ʵ��Id</param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int?[] tenantIds, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// ��ȡ����-�첽
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(long userId)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// �Ƿ���-�첽
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityTypeName">ʵ����������</param>
        /// <param name="entityId">ʵ��Id</param>
        /// <returns></returns>
        public Task<bool> IsSubscribedAsync(long userId, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// �����û�֪ͨ״̬-�첽
        /// </summary>
        /// <param name="userNotificationId">�û�֪ͨId</param>
        /// <param name="state">�û�֪ͨ״̬</param>
        /// <returns></returns>
        public Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ����ȫ���û�֪ͨ״̬-�첽
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="state">�û�֪ͨ״̬</param>
        /// <returns></returns>
        public Task UpdateAllUserNotificationStatesAsync(long userId, UserNotificationState state)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ���û�֪ͨ-�첽
        /// </summary>
        /// <param name="userNotificationId">�û�֪ͨId</param>
        /// <returns></returns>
        public Task DeleteUserNotificationAsync(Guid userNotificationId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ��ȫ���û�֪ͨ-�첽
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        public Task DeleteAllUserNotificationsAsync(long userId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ�û�֪ͨ-�첽
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="state">״̬</param>
        /// <param name="skipCount">������</param>
        /// <param name="maxResultCount">�������</param>
        /// <returns></returns>
        public Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(long userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            return Task.FromResult(new List<UserNotificationInfoWithNotificationInfo>());
        }

        /// <summary>
        /// ��ȡ�û�֪ͨ��-�첽
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        public Task<int> GetUserNotificationCountAsync(long userId, UserNotificationState? state = null)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ�û�֪ͨ��֪ͨΪ��-�첽
        /// </summary>
        /// <param name="userNotificationId">�û�֪ͨId</param>
        /// <returns></returns>
        public Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(Guid userNotificationId)
        {
            return Task.FromResult((UserNotificationInfoWithNotificationInfo)null);
        }
    }
}