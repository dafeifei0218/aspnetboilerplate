namespace Abp.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="IAbpSession"/>.
    /// Abp会话扩展类
    /// </summary>
    public static class AbpSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="AbpException"/> if <see cref="IAbpSession.UserId"/> is null.
        /// 获取当前用户Id，如果为空，抛出AbpException异常
        /// </summary>
        /// <param name="session">Session object. 会话对象</param>
        /// <returns>Current User's Id. 当前用户Id</returns>
        public static long GetUserId(this IAbpSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new AbpException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }

        /// <summary>
        /// Gets current Tenant's Id.
        /// Throws <see cref="AbpException"/> if <see cref="IAbpSession.TenantId"/> is null.
        /// 获取当前用户Id，如果为空，抛出AbpException异常
        /// </summary>
        /// <param name="session">Session object. 会话对象</param>
        /// <returns>Current Tenant's Id. 当前用户Id</returns>
        /// <exception cref="AbpException"></exception>
        public static int GetTenantId(this IAbpSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new AbpException("Session.TenantId is null! Possible problems: No user logged in or current logged in user in a host user (TenantId is always null for host users).");
            }

            return session.TenantId.Value;
        }
    }
}