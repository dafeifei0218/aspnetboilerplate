using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// 审计实体抽象类
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity 实体的主键</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// 最后修改实体的时间。
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// 最后修改实体的该用户。
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
    }
}