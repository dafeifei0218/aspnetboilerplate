using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Null pattern implementation of <see cref="IRealTimeNotifier"/>.
    /// 空实时通知类，实现<see cref="IRealTimeNotifier"/>实时通知接口。
    /// </summary>
    public class NullRealTimeNotifier : IRealTimeNotifier
    {
        /// <summary>
        /// Gets single instance of <see cref="NullRealTimeNotifier"/> class.
        /// 获取单例<see cref="NullRealTimeNotifier"/>空实时通知类实例
        /// </summary>
        public static NullRealTimeNotifier Instance { get { return SingletonInstance; } }
        private static readonly NullRealTimeNotifier SingletonInstance = new NullRealTimeNotifier();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userNotifications">用户通知</param>
        /// <returns></returns>
        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            return Task.FromResult(0);
        }
    }
}