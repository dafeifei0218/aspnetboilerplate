using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Interface to send real time notifications to users.
    /// 实时通知接口，向用户发送实时通知的接口。
    /// </summary>
    public interface IRealTimeNotifier
    {
        /// <summary>
        /// This method tries to deliver real time notifications to specified users.
        /// If a user is not online, it should ignore him.
        /// 发送通知-异步，
        /// 此方法尝试将实时通知发送给特定的用户。
        /// 如果一个用户不在线，它应该忽略此用户。
        /// </summary>
        /// <param name="userNotifications">用户通知</param>
        Task SendNotificationsAsync(UserNotification[] userNotifications);
    }
}