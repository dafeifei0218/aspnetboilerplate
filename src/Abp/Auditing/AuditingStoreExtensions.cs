using Abp.Threading;

namespace Abp.Auditing
{
    /// <summary>
    /// Extension methods for <see cref="IAuditingStore"/>.
    /// 审计存储扩展类
    /// </summary>
    public static class AuditingStoreExtensions
    {
        /// <summary>
        /// Should save audits to a persistent store.
        /// 保存
        /// </summary>
        /// <param name="auditingStore">Auditing store 审计存储</param>
        /// <param name="auditInfo">Audit informations 审计信息</param>
        public static void Save(this IAuditingStore auditingStore, AuditInfo auditInfo)
        {
            AsyncHelper.RunSync(() => auditingStore.SaveAsync(auditInfo));
        }
    }
}