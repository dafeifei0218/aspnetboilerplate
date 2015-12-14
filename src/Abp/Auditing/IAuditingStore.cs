using System.Threading.Tasks;

namespace Abp.Auditing
{
    /// <summary>
    /// This interface should be implemented by vendors to
    /// make auditing working.
    /// Default implementation is <see cref="SimpleLogAuditingStore"/>.
    /// 审计存储接口 
    /// </summary>
    public interface IAuditingStore
    {
        /// <summary>
        /// Should save audits to a persistent store.
        /// 保存审计，应将审计保存到持久存储。
        /// </summary>
        /// <param name="auditInfo">Audit informations 审计信息</param>
        Task SaveAsync(AuditInfo auditInfo);
    }
}