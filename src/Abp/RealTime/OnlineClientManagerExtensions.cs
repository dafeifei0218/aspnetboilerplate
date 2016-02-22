namespace Abp.RealTime
{
    /// <summary>
    /// Extension methods for <see cref="IOnlineClientManager"/>.
    /// 在线客户端管理扩展类
    /// </summary>
    public static class OnlineClientManagerExtensions
    {
        /// <summary>
        /// Determines whether the specified user is online or not.
        /// 确定指定的用户是否在线
        /// </summary>
        /// <param name="onlineClientManager">The online client manager. 在线客户端管理类</param>
        /// <param name="userId">User id. 用户Id</param>
        public static bool IsOnline(IOnlineClientManager onlineClientManager,long userId)
        {
            return onlineClientManager.GetByUserIdOrNull(userId) != null;
        }
    }
}