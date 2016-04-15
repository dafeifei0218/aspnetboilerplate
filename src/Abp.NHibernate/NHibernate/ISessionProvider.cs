using NHibernate;

namespace Abp.NHibernate
{
    /// <summary>
    /// Session会话提供者
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// 会话
        /// </summary>
        ISession Session { get; }
    }
}