namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface ads <see cref="IDeletionAudited"/> to <see cref="IAudited"/> for a fully audited entity.
    /// 全部审计实体
    /// </summary>
    public interface IFullAudited : IAudited, IDeletionAudited
    {
        
    }
}