using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Implements <see cref="IFullAudited{TUser}"/> to be a base class for full-audited entities.
    /// 全部审计实体
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity 实体主键</typeparam>
    /// <typeparam name="TUser">Type of the user 用户类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey, TUser>, IFullAudited<TUser>
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Is this entity Deleted?
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Reference to the deleter user of this entity.
        /// 实体删除的用户
        /// </summary>
        [ForeignKey("DeleterUserId")]
        public virtual TUser DeleterUser { get; set; }

        /// <summary>
        /// Which user deleted this entity?
        /// 该实体的删除用户Id
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time of this entity.
        /// 该实体的删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
}
