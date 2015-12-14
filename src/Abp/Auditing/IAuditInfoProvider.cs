namespace Abp.Auditing
{
    /// <summary>
    /// Provides an interface to provide audit informations in the upper layers.
    /// 审计信息提供者接口，提供一个接口来提供上层的审计信息
    /// </summary>
    public interface IAuditInfoProvider
    {
        /// <summary>
        /// Called to fill needed properties.
        /// 填充所需的属性
        /// </summary>
        /// <param name="auditInfo">Audit info that is partially filled 审计信息，部分填充的审计信息</param>
        void Fill(AuditInfo auditInfo);
    }
}