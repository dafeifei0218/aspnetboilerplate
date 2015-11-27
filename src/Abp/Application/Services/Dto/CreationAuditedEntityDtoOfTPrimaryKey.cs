using System;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="ICreationAudited"/> interface.
    /// �������ʵ�����ݴ������
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key ��������</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>, ICreationAudited
    {
        /// <summary>
        /// Creation date of this entity.
        /// ʵ��Ĵ���ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Creator user's id for this entity.
        /// ʵ����û�Id
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        protected CreationAuditedEntityDto()
        {
            CreationTime = Clock.Now;
        }
    }
}