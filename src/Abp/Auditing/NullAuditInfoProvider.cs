namespace Abp.Auditing
{
    /// <summary>
    /// Null implementation of <see cref="IAuditInfoProvider"/>.
    /// 空审计信息提供者
    /// </summary>
    /// <remarks>
    ///  空的IAuditInfoProvider实现，这个是ABP中的缺省的IAuditInfoProvider的实现。
    /// </remarks>
    internal class NullAuditInfoProvider : IAuditInfoProvider
    {
        public void Fill(AuditInfo auditInfo)
        {
            
        }
    }
}