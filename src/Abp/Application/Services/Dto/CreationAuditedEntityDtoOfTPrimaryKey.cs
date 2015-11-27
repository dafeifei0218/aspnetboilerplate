using System;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="ICreationAudited"/> interface.
    /// 创建审计实体数据传输对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key 主键类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>, ICreationAudited
    {
        /// <summary>
        /// Creation date of this entity.
        /// 实体的创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Creator user's id for this entity.
        /// 实体的用户Id
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        protected CreationAuditedEntityDto()
        {
            CreationTime = Clock.Now;
        }
    }
}