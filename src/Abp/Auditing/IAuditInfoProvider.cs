namespace Abp.Auditing
{
    /// <summary>
    /// Provides an interface to provide audit informations in the upper layers.
    /// 审计信息提供者接口，提供一个接口来提供上层的审计信息
    /// </summary>
    /// <remarks>
    /// 这个接口定义一个方法用于完善AuditInfo对象。
    /// 为什么要定义一个这样的接口和方法呢？ABP核心模块处于最底层， 有些上层的信息在这一层无法直接取得（比如浏览器信息）。
    /// ABP的做法是在上层实现IAuditInfoProvider，然后将其register到底层的容器中。
    /// 处于底层ABP的核心模块则从resolve出这个对象，然后调用该对象的fill方法来完善AuditInfo。
    /// </remarks>
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