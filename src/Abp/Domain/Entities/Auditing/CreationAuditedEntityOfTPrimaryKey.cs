using System;
using Abp.Timing;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// �������ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity ʵ�������</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        /// <summary>
        /// Creation time of this entity.
        /// ����ʵ��ʱ��
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Creator of this entity.
        /// ����ʵ���û�
        /// </summary>
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        protected CreationAuditedEntity()
        {
            CreationTime = Clock.Now;
        }
    }
}