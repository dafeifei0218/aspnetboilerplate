using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// ֪ͨ�ַ��ӿڣ��������û��ַ�֪ͨ��
    /// </summary>
    /// <remarks>
    /// ���ڷַ�Notification��User��Ҳ���ǽ���Notification��User�Ĺ�ϵ��
    /// �ڷַ�Notification��ĳ��Userǰ����User��NotificationSetting�����������ΪTrue�ͽ���Notification�͸�User�Ĺ�ϵ
    /// </remarks>
    public interface INotificationDistributer : IDomainService
    {
        /// <summary>
        /// Distributes given notification to users.
        /// �ַ����û���֪ͨ��
        /// </summary>
        /// <param name="notificationId">The notification id. ֪ͨId</param>
        Task DistributeAsync(Guid notificationId);
    }
}