namespace Abp.Auditing
{
    /// <summary>
    /// Null implementation of <see cref="IAuditInfoProvider"/>.
    /// 空审计信息提供者
    /// </summary>
    internal class NullAuditInfoProvider : IAuditInfoProvider
    {
        public void Fill(AuditInfo auditInfo)
        {
            
        }
    }
}