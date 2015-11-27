using System;
using Abp.Domain.Entities.Auditing;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IAudited{TUser}"/> interface.
    /// ���ʵ�����ݴ������
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key ��������</typeparam>
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// ����޸�ʵ���ʱ�䡣
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// ����޸�ʵ��ĸ��û���
        /// </summary>
        public long? LastModifierUserId { get; set; }
    }
}