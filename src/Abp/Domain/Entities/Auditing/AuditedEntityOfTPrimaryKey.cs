using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// ���ʵ�������
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity ʵ�������</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// ����޸�ʵ���ʱ�䡣
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// ����޸�ʵ��ĸ��û���
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
    }
}