namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IAudited"/> interface for user.
    /// 审计接口
    /// </summary>
    /// <typeparam name="TUser">Type of the user 用户类型</typeparam>
    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}