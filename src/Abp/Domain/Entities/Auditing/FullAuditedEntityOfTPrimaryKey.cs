using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Implements <see cref="IFullAudited"/> to be a base class for full-audited entities.
    /// ȫ�����ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity ʵ������</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// Is this entity Deleted?
        /// �Ƿ�ɾ��ʵ�壿
        /// </summary>
        public virtual bool IsDeleted { get; set; }
        
        /// <summary>
        /// Which user deleted this entity?
        /// ɾ��ʵ����û�Id
        /// </summary>
        public virtual long? DeleterUserId { get; set; }
        
        /// <summary>
        /// Deletion time of this entity.
        /// ɾ��ʵ���ʱ��
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
}
