namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IDeletionAudited"/> interface for user.
    /// 删除审计接口
    /// </summary>
    /// <typeparam name="TUser">Type of the user 用户类型</typeparam>
    public interface IDeletionAudited<TUser> : IDeletionAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the deleter user of this entity.
        /// 实体删除的用户
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}