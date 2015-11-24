namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IFullAudited"/> interface for user.
    /// 全部审计实体
    /// </summary>
    /// <typeparam name="TUser">Type of the user 用户类型</typeparam>
    public interface IFullAudited<TUser> : IFullAudited, IDeletionAudited<TUser> 
        where TUser : IEntity<long>
    {

    }
}