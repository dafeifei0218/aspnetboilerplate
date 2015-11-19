namespace Abp.Domain.Entities
{
    /// <summary>
    /// Implement this interface for an entity which may optionally have TenantId.
    /// 租户接口，TenantId为可空类型
    /// </summary>
    public interface IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// 实体的租户Id
        /// </summary>
        int? TenantId { get; set; }
    }
}