namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IModificationAudited"/> interface for user.
    /// 修改审计接口
    /// </summary>
    /// <typeparam name="TUser">Type of the user 用户实体</typeparam>
    public interface IModificationAudited<TUser> : IModificationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// 最后修改用户实体
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}