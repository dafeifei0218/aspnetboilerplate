namespace Abp.Domain.Entities
{
    /// <summary>
    /// Implement this interface for an entity which must have TenantId.
    /// 租户接口，TenantId为不可空类型
    /// </summary>
    public interface IMustHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// 实体的租户Id
        /// </summary>
        int TenantId { get; set; }
    }
}