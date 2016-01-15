using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// Implements <see cref="IFullAudited{TUser}"/> to be a base class for full-audited entities.
    /// ȫ�����ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity ʵ������</typeparam>
    /// <typeparam name="TUser">Type of the user �û�����</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey, TUser>, IFullAudited<TUser>
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Is this entity Deleted?
        /// �Ƿ�ɾ��
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Reference to the deleter user of this entity.
        /// ʵ��ɾ�����û�
        /// </summary>
        [ForeignKey("DeleterUserId")]
        public virtual TUser DeleterUser { get; set; }

        /// <summary>
        /// Which user deleted this entity?
        /// ��ʵ���ɾ���û�Id
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time of this entity.
        /// ��ʵ���ɾ��ʱ��
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
}
