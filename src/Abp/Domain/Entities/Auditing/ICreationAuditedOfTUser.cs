namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="ICreationAudited"/> interface for user.
    /// 创建审计接口
    /// </summary>
    /// <typeparam name="TUser">Type of the user 用户类型</typeparam>
    public interface ICreationAudited<TUser> : ICreationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// 创建用户
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}