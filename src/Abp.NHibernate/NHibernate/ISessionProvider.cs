using NHibernate;

namespace Abp.NHibernate
{
    /// <summary>
    /// Session�Ự�ṩ��
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// �Ự
        /// </summary>
        ISession Session { get; }
    }
}