using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Implements <see cref="IFullAudited"/> to be a base class for full-audited entities.
    /// 全部审计实体
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity 实体主键</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// Is this entity Deleted?
        /// 是否删除实体？
        /// </summary>
        public virtual bool IsDeleted { get; set; }
        
        /// <summary>
        /// Which user deleted this entity?
        /// 该实体的删除用户Id，哪个用户删除了该实体
        /// </summary>
        public virtual long? DeleterUserId { get; set; }
        
        /// <summary>
        /// Deletion time of this entity.
        /// 该实体的删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
}
